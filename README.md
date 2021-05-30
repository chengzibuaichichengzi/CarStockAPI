# CarStockAPI
A web api built in ASP.NET Core that that allows dealers to manage their car stocks.

## Running Steps
1. Pull docker image from docker hub: 
[https://hub.docker.com/r/persistentpzk/car-stock-api/](https://hub.docker.com/r/persistentpzk/car-stock-api/)
```
docker pull persistentpzk/car-stock-api
```

2. Run the local docker image as a container
```
docker run -d -p 80:80 --name carstockapi persistentpzk/car-stock-api
```

3. The web api will run in [http://localhost:80/](http://localhost:80/). Open [http://localhost:80/](http://localhost:80/) in browser will default route to swagger UI page ([http://localhost:80/index.html](http://localhost:80/index.html))

## API Authorization
All Car api endpoints need to include an access token in the header. Dealer will use their own client Id and secret key to generate the token. Each dealer can not access/modify other’s stocks.


To generate the token, use **/authenticate** (included in the Swagger UI) with following 2 credentials:
- Dealer 1:
```
{
  "clientId": "dealerClientId",
  "clientSecret": "dealerSecretKey"
}
```
- Dealer 2:
```
{
  "clientId": "anotherDealerClientId",
  "clientSecret": "anotherKey"
}
```

To test api functions using Swagger, put the generated token **Authorize** section.
To test api functions through Postman, include generated token in request header:
```
Key: Tokenheader
Value: <token>
```

## Simulated Database
The api is using an in-memory database for testing. When the application start running, it will automatically generate some test car data to both dealers.
