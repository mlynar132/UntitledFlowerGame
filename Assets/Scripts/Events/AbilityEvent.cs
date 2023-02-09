using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/Ability Event")]
public class AbilityEvent : ScriptableEvent<AbilityUsed> {}