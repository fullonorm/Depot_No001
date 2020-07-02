using UnityEngine;
using UnityEngine.UI;

public class AlertViewOptions
{
    // 취소 버튼을 눌렀을 때 실행되는 대리자
    public System.Action cancelButtonDelegate;
   
    // 확인 버튼을 눌렀을 때 실행되는 대리자
    public System.Action okButtonDelegate;  
}

public class AlertViewController : MonoBehaviour
{
    [Header("AlertView")]
    // 타이틀을 표시 
    public Text titleLabel;
    // 메시지를 표시 
    public Text messageLabel;
    // 취소버튼
    public Button cancelButton;
    // 확인버튼
    public Button okButton;

    private static GameObject prefab;
    // 취소 버튼 클릵시 실행되는 대리자 지정
    private System.Action cancelButtonDelegate;
    // 확인 버튼 클릭시 실행되는 대리자 지정
    private System.Action okButtonDelegate;

    // 알임 뷰를 표시하는 static 메서드 
    public static AlertViewController Show(
        string title, string message, AlertViewOptions options = null)
    {
        if (prefab == null)
        {
            // 프리팹을 읽어 들인다. 
            prefab = Resources.Load("AlertView") as GameObject;
        }

        GameObject go = Instantiate(prefab) as GameObject;
        AlertViewController alertView = go.GetComponent<AlertViewController>();
        alertView.UpadateContent(title, message, options);

        return alertView;
    }

    // 읽어 들인 내용을 갱신하는 메서드 
    public void UpadateContent(
        string title, string message, AlertViewOptions options = null)
    {
        //타이틀과 메시지를 설정 
        titleLabel.text = title;
        messageLabel.text = message;

        if (options != null)
        {
            cancelButton.gameObject.SetActive(true);
            cancelButtonDelegate = options.cancelButtonDelegate;
            okButton.gameObject.SetActive(true);
            okButtonDelegate = options.okButtonDelegate;
        }

        else
        {
            // 표시 옵션이 지정된 경우 기본 버튼을 표시한다.
            cancelButton.gameObject.SetActive(true);
            okButton.gameObject.SetActive(false);
        }
    }

    // 알림창을 닫는 메서드 
    public void Dismiss()
    {
        Destroy(gameObject);
    }

    //취소 버튼을 눌렀을 때 호출되는 메서드 
    public void OnPressCancelButton()
    {
        if (cancelButtonDelegate != null)
        {
            cancelButtonDelegate.Invoke();
        }
        Dismiss();
    }

    //확인 버튼을 툴렀을 때 호출되는 메서드
    public void OnPressOkButton()
    {
        if (okButtonDelegate != null)
        {
            okButtonDelegate.Invoke();
        }
        Dismiss();
    }

}
