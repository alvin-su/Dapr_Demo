1、启动 dpar 命令：
dapr run --dapr-http-port 3501 --app-port 5002  --app-id statedemo dotnet  StateDemo.dll
2、HttpPost 写入：
http://localhost:3501/v1.0/invoke/statedemo/method/state
3、HttpGet 获取:
http://localhost:3501/v1.0/state/statestore/guid
5、HttpGet 获取WithEtag：
http://localhost:3501/v1.0/invoke/statedemo/method/state/withetag
