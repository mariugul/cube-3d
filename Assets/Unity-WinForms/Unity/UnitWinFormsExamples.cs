using System.Windows.Forms;
using UnityEngine;

using UnityWinForms.Examples;

namespace UnityWinForms
{
    public class UnitWinFormsExamples : MonoBehaviour
    {
        public static Material s_chartGradient;
        public Material ChartGradient;

        private void Start()
        {
            s_chartGradient = ChartGradient;
            
            var form = new FormExamples();

            form.Show();
        }
    }
}