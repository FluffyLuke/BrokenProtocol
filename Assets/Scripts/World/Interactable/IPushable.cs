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
        // This function set, if object can be pushed at this moment
        public abstract void EnablePushable(bool enable);
        // This two functions inform object, that player is pushing it
        public abstract void EnterPushingState();
        public abstract void ExitPushingState();
}