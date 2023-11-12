using UnityEngine;

namespace RFW
{
    public class BootStrap : MonoBehaviour
    {
        [SerializeField] private string _pathToSystems = "Systems/";

        private ILevelGenerator _levelGenerator = null;


        private void Awake()
        {

        }
    }
}