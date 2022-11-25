using Leopotam.Ecs;
using UnityEngine;
using ZombieSurvivors.Systems;

namespace ZombieSurvivors {
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        public Configuration configuration;

        void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your systems here, for example:
                .Add (new InitializeFieldSystem ())
                .Add (new InitializeCameraSystem ())
                .Add (new InitializePlayerSystem ())
                .Add (new InitializeEnemyPoolSystem ())
                .Add (new CreateFieldViewSystem ())
                .Add (new SpawnEnemySystem ())
                .Add (new KeyboardInputSystem ())
                .Add (new MouseInputSystem ())
                .Add (new EnemyAISystem ())
                .Add (new CommandHandleSystem ())
                .Add (new MoveEngineSystem ())
                .Add (new RotateEngineSystem ())
                .Add (new CameraControlSystem ())

                // register one-frame components (order is important), for example:
                //.OneFrame<CommandMoveTo> ()
                //.OneFrame<CommandRotateTo> ()
                
                // inject service instances here (order doesn't important), for example:
                .Inject (configuration)
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}