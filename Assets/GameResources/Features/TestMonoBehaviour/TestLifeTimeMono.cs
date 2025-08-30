namespace CanvasTest.Features.TestMonoBehaviour
{
    using UnityEngine;

    /// <summary>
    /// Test class that demonstrates Life Cycle of MonoBehaviour
    /// </summary>
    public class TestLifeTimeMono : MonoBehaviour
    {
        protected virtual void Awake() => Debug.Log("Awake");
        
        private void OnEnable() => Debug.Log("OnEnable");

        private void Start() => Debug.Log("Start");

        private void Update() => Debug.Log($"Update {Time.deltaTime}");

        private void FixedUpdate() => Debug.Log($"FixedUpdate {Time.deltaTime}");

        private void OnDisable() => Debug.Log($"OnDisable");

        private void OnDestroy() => Debug.Log($"OnDestroy");

    }
    
}
