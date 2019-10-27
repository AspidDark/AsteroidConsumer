using UnityEngine;

public class DataReceiver : MonoBehaviour {

    public string incData;

    public void ReceiveData(object data)
    {
        incData = data as string;
        // тут парсить значения и активировать объект и переинстантиейтить на самом объекте класс движения сделать тот класс который использует движение в разные строны
    }
}
