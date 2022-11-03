using UnityEngine;

namespace BarthaSzabolcs.IsometricAiming
{
    public class IsometricAimingWithGizmos : MonoBehaviour
    {
        #region Datamembers

        #region Editor Settings

        [Header("Aim")]
        [SerializeField] private bool aim;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private bool ignoreHeight;
        [SerializeField] private Transform aimedTransform;

        [Header("Laser")]
        [SerializeField] private LineRenderer laserRenderer;
        [SerializeField] private LayerMask laserMask;
        [SerializeField] private float laserLength;

        [Header("Projectile")]
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Transform prefabSpawn;

        

        #endregion
        #region Private Fields

        private Camera mainCamera;

        #endregion

        #endregion


        #region Methods

        #region Unity Callbacks

        private void Start()
        {
            mainCamera = Camera.main;

            if (laserRenderer != null)
            {
                laserRenderer.SetPositions(new Vector3[]{
                    Vector3.zero,
                    Vector3.zero
                });
            }
        }

        private void Update()
        {
            Aim();
            RefreshLaser();
            Shoot();
            
            
        }

      

        #endregion

        private void Aim()
        {
            if (aim == false)
            {
                return;
            }

            var (success, position) = GetMousePosition();
            if (success)
            {
                // Direction is usually normalized, 
                // but it does not matter in this case.
                var direction = position - aimedTransform.position;

                if (ignoreHeight)
                {
                    // Ignore the height difference.
                    direction.z = 0;
                }

                // Make the transform look at the mouse position.
                aimedTransform.forward = direction;
            }
        }
        
        private (bool success, Vector3 position) GetMousePosition()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
            {
                return (success: true, position: hitInfo.point);
            }
            else
            {
                return (success: false, position: Vector3.zero);
            }
        }

        private void Shoot()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var projectile = Instantiate(projectilePrefab, prefabSpawn.position, Quaternion.identity);
                projectile.transform.forward = aimedTransform.forward;
            }
        }

        private void RefreshLaser()
        {
            if (laserRenderer == null)
            {
                return;
            }

            Vector3 lineEnd;

            if (Physics.Raycast(prefabSpawn.position, prefabSpawn.forward, out var hitinfo,  laserLength, laserMask))
            {
                lineEnd = hitinfo.point;
            }
            else
            {
                lineEnd = prefabSpawn.position + aimedTransform.forward * laserLength;
            }

            laserRenderer.SetPosition(1, aimedTransform.InverseTransformPoint(lineEnd));
        }

      
     

        #endregion
    }
}