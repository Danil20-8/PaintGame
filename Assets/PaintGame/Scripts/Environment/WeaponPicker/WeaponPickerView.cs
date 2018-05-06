using PaintGame.Character;
using PaintGame.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PaintGame.Environment.WeaponPicker
{
    public class WeaponPickerView : MonoBehaviour
    {
        [SerializeField]
        float rotationSpeed;

        Transform weaponView;

        public void InitializeView(Weapon weaponPrefab)
        {
            var viewPosition = weaponPrefab.GetComponentsInChildren<MeshFilter>()
                .SelectMany(f => f.sharedMesh.vertices.Select(v => f.transform.localToWorldMatrix.MultiplyPoint3x4(v) - weaponPrefab.transform.position))
                .Center();
            var view = Instantiate(weaponPrefab, transform.position - viewPosition, transform.rotation, transform);

            foreach (var col in view.GetComponentsInChildren<Collider>())
                Destroy(col);

            weaponView = view.transform;
            weaponView.localPosition = -viewPosition;
        }

        void Update()
        {
            weaponView.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
