using TMPro;
using Tools.Pool;
using UnityEngine;

namespace UI
{
    public class DamageText : PoolableObject
    {
        [Header("Animation")]
        [SerializeField] private float moveSpeed = 2f;
        [SerializeField] private float lifetime = 1f;

        [Header("Random")]
        [SerializeField] private float randomX = 0.5f;
        [SerializeField] private float randomY = 0.3f;

        [Header("References")]
        [SerializeField] private TextMeshPro textMesh;

        private Vector3 moveDirection;

        private Color textColor;

        private float timer;

        public void Show(int damage, Vector3 worldPosition)
        {
            transform.position = worldPosition;

            textMesh.text = $"-{damage}";

            moveDirection = new Vector3(Random.Range(-randomX, randomX), 1f + Random.Range(0f, randomY), 0f);

            timer = 0f;

            textColor = textMesh.color;

            textColor.a = 1f;

            textMesh.color = textColor;
        }

        private void Update()
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            timer += Time.deltaTime;

            var progress = timer / lifetime;
            textColor.a = Mathf.Lerp(1f, 0f, progress);

            textMesh.color = textColor;

            if (timer >= lifetime)
            {
                ReturnToPool();
            }
        }

        public override void OnSpawn()
        {
        }

        public override void OnDespawn()
        {
        }
    }
}