using UnityEngine;
using UnityEngine.InputSystem;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Transform cameraAnchor;   // точка прив'язки камери 
    // за якою вона рухається, навколо якої обертається та до якої наближається
    private Vector3 offset;  // вектор взамного розміщення персонажа та камери

    private InputAction lookAction;   // Рухи маніпулятора "миша"
    private float rotAngleY;
    private float rotSensitivityY = 10f;
    private float rotAngleX;
    private float rotSensitivityX = 5f;

    void Start()
    {
        offset = cameraAnchor.position - transform.position ;
        lookAction = InputSystem.actions.FindAction("Look");
        rotAngleY = transform.eulerAngles.y;
        rotAngleX = transform.eulerAngles.x;
    }

    void Update()
    {
        Vector2 lookValue = Time.deltaTime * lookAction.ReadValue<Vector2>();
        //     new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        rotAngleY += rotSensitivityY * lookValue.x;
        rotAngleX -= rotSensitivityX * lookValue.y;

        transform.eulerAngles = new Vector3(rotAngleX, rotAngleY, 0f);  // самого лише
        // обертання камери недостатньо, оскільки вона губить персонажа з 
        // поля зору через наявність зміщення offset -- його теж треба 
        // повертати разом з поворотом камери

        transform.position = cameraAnchor.position - // offset : без корекції на поворот
            Quaternion.Euler(0f, rotAngleY, 0f) * offset;
    }
}
/* Управління камерою.
 * Основа - положення персонажа, а також
 * рухи миші, які обертають камеру.
 */
/* Д.З. Підібрати граничні кути для повороту камери 
 * по вертикалі
 * - не бачить горизонт
 * - не випускає персонаж з поля зору
 * Впровадити обмеження на обертання згідно з визначеними
 * граничними кутами.
 */
