using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Burst;

    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class GetPlayerInputSystem : SystemBase
    {
        private MovementActions MovementActions;
        private Entity _playerEntity;

        protected override void OnCreate()
        {
        RequireForUpdate<PlayerTag>();
        RequireForUpdate<PlayerMovementInput>();
        Debug.Log("<b> <size=13> <color=#9DF155>Info : 3 SetDataSystem : Setting Data .</color> </size> </b>");
        MovementActions = new MovementActions();
        }

        protected override void OnStartRunning()
        {
        MovementActions.Enable();
        MovementActions.Map.Shoot.performed += OnPlayerShoot;


        _playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();

    }

        protected override void OnUpdate()
        {
            var MoveInput = MovementActions.Map.PlayerMovement.ReadValue<Vector2>();

            SystemAPI.SetSingleton(new PlayerMovementInput { Value = MoveInput });
        }

        protected override void OnStopRunning()
        {
            MovementActions.Map.Shoot.performed -= OnPlayerShoot;
            MovementActions.Disable();

            _playerEntity = Entity.Null;
        }

        private void OnPlayerShoot(InputAction.CallbackContext obj)
        {
            if (!SystemAPI.Exists(_playerEntity)) return;

            SystemAPI.SetComponentEnabled<FireProjectileTag>(_playerEntity, true);
        }
    }