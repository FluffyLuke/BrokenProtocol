using UnityEngine;

public abstract class IPushable : MonoBehaviour
{
        protected InputSystem_Actions input = null;
        public float side = 0;
        void Awake() {
                input = new InputSystem_Actions();
        }
        
        private void OnDestroy() {
                input.Dispose();
        }

        public abstract void EnablePushable();
        public abstract void DisablePushable();
}