namespace Finnce_Api.Infrastructure.ExternalServices
{
    public class TinkEndpoints
    {
        private readonly HttpClient httpClient;
        private readonly string urlGenerateToken = "https://api.tink.com/api/v1/oauth/token";
        private readonly string urlCreateUser = "https://api.tink.com/api/v1/user/create";
        private readonly string urlGenerateCode = "https://api.tink.com/api/v1/oauth/token";
        private readonly string urlCreateGrant = "https://api.tink.com/api/v1/oauth/authorization-grant/delegate";
        private readonly string urlGetAccounts = "https://api.tink.com/data/v2/accounts";
        private readonly string urlGetTransactions = "https://api.tink.com/data/v2/transactions";
        private readonly string ClientId;
        private readonly string SecretId;
        private readonly string actor_client_id;
        private readonly string market;
        private readonly string locale;
        private readonly string id_hint;
        private readonly string scope;
        private readonly string redirectUrl;

        public TinkEndpoints(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
             .Build();
            if (configuration != null)
            {
                this.ClientId = configuration["TinkApi:ClientId"];
                this.SecretId = configuration["TinkApi:SecretId"];
                this.actor_client_id = configuration["TinkApi:actor_client_id"];
                this.locale = configuration["TinkApi:locale"];
                this.market = configuration["TinkApi:market"];
                this.id_hint = configuration["TinkApi:id_hint"];
                this.scope = configuration["TinkApi:scope"];
                this.redirectUrl = configuration["TinkApi:redirectUrl"];
            }

        }

        // Faltam validações de dados
        /// <summary>
        ///  https://docs.tink.com/resources/transactions/continuous-connect-to-a-bank-account#build-the-url 
        ///1, 2, 3 The function call the other functions to complete continuos acess in api tink, step 1, 2, 3 in documentation 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<string> ContinuosAcessFunction(string userId)
        {
            // Step 1
            string createUser = await CreateUser(userId);

            // Step 2
            string authorizationgrant = await GenerateUserAuthorizationCodeAndGrantUserAccess(userId);

            // Step 3 
            string urlTink = OrganizeUrlTink(authorizationgrant, userId);


            return urlTink;

        }



        /// <summary>
        /// 1  Is this a step-- 1 -- in the documentation
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<string> CreateUser(string userId)
        {

            string autorization = await GetAuthorization();
            string newUser = "";
            if (autorization != null)
            {
                newUser = await CreateUser(userId, autorization);

            }
            else
            {
                autorization = "Failed to generate acess Token";
                return autorization;
            }

            return newUser;
        }


        /// <summary>
        ///  1.1 This function is essential to get autorization in tink console in the documentation is the step -- 1.1 --; " 1.1 Authorize your app"
        /// </summary>
        /// <returns></returns>
        private async Task<string> GetAuthorization()
        {

            var queryString = new StringBuilder();
            queryString.Append("client_id=" + this.ClientId);
            queryString.Append("&client_secret=" + this.SecretId);
            queryString.Append("&grant_type=client_credentials");
            queryString.Append("&scope=user:create");


            var content = new StringContent(queryString.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await httpClient.PostAsync(urlGenerateToken, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }


        /// <summary>
        ///1.2 This function is essential to create user in tink console, in documentation is the step 1.2; "1.2 Create a user"
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="autorization"></param>
        /// <returns></returns>
        /// 
        private async Task<string> CreateUser(string userId, string autorization)
        {


            var accessToken = JObject.Parse(autorization)["access_token"].ToString();
            var requestData = new
            {
                external_user_id = userId.ToString(),
                locale = this.locale,
                market = this.market
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.PostAsync(urlCreateUser, content);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                var result = "User probability exist, error";
                return result;
            }


        }

        /// <summary>
        ///2 This Function create new acess user and generate code and grant, in the documentation is the step 2
        /// </summary>
        /// <returns></returns>
        private async Task<string> GenerateUserAuthorizationCodeAndGrantUserAccess(string IdUser)
        {

            var authorization = await GenerateCode();

            var codeAcess = await GrantUserAcess(authorization, IdUser);


            return codeAcess;
        }

        /// <summary>
        ///2.1 This function generate token acess to aplication, in the documentation is the step--- 2.1 ---; "2.1 Generate the code"
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private async Task<string> GenerateCode()
        {
            var queryString = new StringBuilder();
            queryString.Append("client_id=" + this.ClientId);
            queryString.Append("&client_secret=" + this.SecretId);
            queryString.Append("&grant_type=client_credentials");
            queryString.Append("&scope=authorization:grant");


            var content = new StringContent(queryString.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await httpClient.PostAsync(urlGenerateCode, content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            return result;
        }


        /// <summary>
        ///2.2 This function is for user access, in the documentation is the step-- 2.2--;"2.2 Grant user access"
        /// </summary>
        /// <returns></returns>
        private async Task<string> GrantUserAcess(string autorization, string userIdFinnce)
        {

            var accessToken = JObject.Parse(autorization)["access_token"].ToString();

            var requestData = new Dictionary<string, string>
                {
                    { "external_user_id", userIdFinnce.ToString() },
                    { "id_hint", this.id_hint },
                    { "actor_client_id",this.actor_client_id },
                    { "scope",this.scope }
                };


            var jsonContent = JsonConvert.SerializeObject(requestData);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.PostAsync(urlCreateGrant, new FormUrlEncodedContent(requestData));
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var code = JObject.Parse(result)["code"].ToString();
                return code;
            }
            else
            {
                var result = "Erro to get acess token to get grant access, error";
                return result;
            }


        }


        /// <summary>
        ///3 In this step is suposed return the link url for client
        /// </summary>
        /// <returns></returns>
        private string OrganizeUrlTink(string UserAuthorizationCode, string userId)
        {

            return "https://link.tink.com/1.0/transactions/connect-accounts?client_id=" + this.ClientId + "&redirect_uri=" + this.redirectUrl + "?userId=" + userId + "&authorization_code=" + UserAuthorizationCode + "&market=" + this.market + "&locale=" + this.locale;


        }

        // Fetch data from Api

        /// <summary>
        /// 4.1 Recebe o código depois do utilizador já ter conectado os seus dados financeira na Tink(atraves do link)
        /// e depois retorna o oauth code para conseguir aceder a account e as transações; Step --4--
        /// </summary>
        /// <param name="authCode"></param>
        /// <returns></returns>
        /// 
        public async Task<string> GetTokenFromAuthCode(string authCode)
        {
            var queryString = new StringBuilder();
            queryString.Append("client_id=" + this.ClientId);
            queryString.Append("&client_secret=" + this.SecretId);
            queryString.Append("&grant_type=authorization_code");
            queryString.Append("&code=" + authCode);

            var content = new StringContent(queryString.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await httpClient.PostAsync(urlGenerateToken, content);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }

        /// <summary>
        /// Retorna as contas associados ao token de acesso recebido 
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        /// 
        public async Task<string> GetTinkUserAccounts(string accessToken)
        {
            //string authorization = await GetTokenFromAuthCode(code);
            //var accessToken = JObject.Parse(authorization)["access_token"].ToString();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync(urlGetAccounts);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                var result = "Error to get acess token!";
                return result;
            }


        }

        /// <summary>
        /// Retorna as transações associadas ao token de acesso recebido
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public async Task<string> GetTinkUserTransactions(string accessToken)
        {

            //string authorization = await GetTokenFromAuthCode(code); 
            //var accessToken = JObject.Parse(authorization)["access_token"].ToString();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await httpClient.GetAsync(urlGetTransactions);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                var result = "Error to get acess token!";
                return result;
            }


        }


        public async Task<string> GetCodeFromCallback()
        {
            string callbackUrl = "http://localhost:3000/callback";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(callbackUrl);

                if (response.IsSuccessStatusCode)
                {
                    // Recuperar os parâmetros do callback
                    string callbackResult = await response.Content.ReadAsStringAsync();

                    // Processar os parâmetros conforme necessário
                    // ...

                    // Retornar os parâmetros encontrados no callback
                    return callbackResult;
                }
                else
                {
                    // Lidar com falhas na requisição
                    return "Erro na requisição: " + response.StatusCode;
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções durante a requisição
                return "Erro: " + ex.Message;
            }
        }
    }



}



