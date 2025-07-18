using System;
using Unity.XR.CoreUtils.Datums;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Theme;

namespace XR.AffordanceSystem
{
    /// <summary>
    /// Affordance state theme data structure for bool affordances. 
    /// </summary>
    [Serializable]
    public class BoolAffordanceTheme : BaseAffordanceTheme<bool>
    {
    }

    /// <summary>
    /// Serializable container class that holds a float affordance theme value or container asset reference.
    /// </summary>
    /// <seealso cref="BoolAffordanceThemeDatum"/>
    [Serializable]
    public class BoolAffordanceThemeDatumProperty : DatumProperty<BoolAffordanceTheme, BoolAffordanceThemeDatum>
    {
        /// <inheritdoc/>
        public BoolAffordanceThemeDatumProperty(BoolAffordanceTheme value) : base(value)
        {
        }

        /// <inheritdoc/>
        public BoolAffordanceThemeDatumProperty(BoolAffordanceThemeDatum datum) : base(datum)
        {
        }
    }

    /// <summary>
    /// <see cref="ScriptableObject"/> container class that holds a bool affordance theme value.
    /// </summary>
    [CreateAssetMenu(fileName = "BoolAffordanceTheme", menuName = "Affordance Theme/Bool Affordance Theme", order = 100)]
    public class BoolAffordanceThemeDatum : Datum<BoolAffordanceTheme>
    {
    }
}