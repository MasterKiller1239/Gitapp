using UnityEngine;

public class TestController : MonoBehaviour
{
    [ContextMenu("Test Get")]
    public async void TestGet()
    {
        var url = "https://api.github.com/search/users?q=Master&page=1";

        var httpClient = new HappyHttpClient(new JsonSerializationOption());

        var result = await httpClient.Get<string>(url);
    }
}
