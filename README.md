# Bithumb.Net
[![NuGet latest version](https://badgen.net/nuget/v/Bithumb.Net/latest)](https://nuget.org/packages/Bithumb.Net)
[![GitHub forks](https://badgen.net/github/forks/dongbin300/Bithumb.Net/)](https://GitHub.com/dongbin300/Bithumb.Net/network/)
![Profile views](https://gpvc.arturio.dev/dongbin300)
<br/><br/>
Bithumb Open API wrapper for .NET

## Documentation
### Official Docs
- official docs: https://apidocs.bithumb.com/reference/%ED%98%84%EC%9E%AC%EA%B0%80-%EC%A0%95%EB%B3%B4-%EC%A1%B0%ED%9A%8C-all
- about API: https://apidocs.bithumb.com/docs/api-%EC%86%8C%EA%B0%9C

### Example
refer to [this](https://github.com/dongbin300/Bithumb.Net/blob/main/Bithumb.Net.Examples/MainWindow.xaml.cs)
<br/>
#### BithumbClient
##### 1. Create a Bithumb client with `connectKey` and `secretKey`
```C#
var connectKey = "your_API_key";
var secretKey = "your_secret_key";
var client = new BithumbClient(connectKey, secretKey);
```

##### 2. Get current BTC price
```C#
var result = client.Public.GetTickerAsync(BithumbPaymentCurrency.KRW, "BTC");
result.Wait();
var price = result.Result.data?.closing_price;
```

#### BithumbSocketClient
##### 1. Create a Bithumb socket client
```C#
var socketClient = new BithumbSocketClient();
```

##### 2. Subscribe to ticker for getting current BTC price
```C#
await socketClient.Streams.SubscribeToTickerAsync( "BTC_KRW", BithumbSocketTickInterval.OneHour, OnMessage).ConfigureAwait(false);
```

##### 3. Implement OnMessage Method
```C#
private void OnMessage(BithumbWebSocketResponse<BithumbWebSocketTicker> obj)
{
    var symbol = obj.content.symbol;
    var price = obj.content.closePrice;
    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, () =>
    {
        SocketTickerText.Text = $"{symbol} \\{price:#,###}";
    });
}
```

##### 4. Unsubscribe to stop getting price
```C#
socketClient.Streams.UnsubscribeToTicker();
```

## Feedback
[Issue](https://github.com/dongbin300/Bithumb.Net/issues)

## Donate
[Donate](https://www.buymeacoffee.com/psS4YtQ)

## Release Notes
- Version 1.0.0 - _2023-05-11_
  - Initial commit
  - Publish on NuGet
  - Write README
