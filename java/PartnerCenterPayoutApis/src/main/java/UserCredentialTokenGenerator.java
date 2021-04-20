import com.microsoft.aad.msal4j.*;
import lombok.Data;

import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.util.HashSet;
import java.util.Properties;
import java.util.Set;
import java.util.concurrent.ExecutionException;
import java.util.function.Consumer;

@Data
public class UserCredentialTokenGenerator {

    private String _tenantId;
    private String _clientId;
    private IPublicClientApplication _clientApp;

    private String get_Authority() {
        return String.format("https://login.microsoftonline.com/%s", get_tenantId());
    }

    private Set<String> get_Scopes() {
        Set<String> scopes = new HashSet<>();
        scopes.add("https://api.partner.microsoft.com/.default");

        return scopes;
    }

    public UserCredentialTokenGenerator() throws IOException{
        fillPropertyValues();
    }



    public String GetToken() throws ExecutionException, InterruptedException, MalformedURLException {
        Init();

        IAccount account = _clientApp.getAccounts().get().iterator().next();

        if (account == null){
            throw new Error("Account isn't set. Validate application configuration");
        }

        SilentParameters silentParameters = SilentParameters.builder(get_Scopes(), account).build();
        return _clientApp.acquireTokenSilently(silentParameters).get().accessToken();
    }

    private void Init() {
        if (_clientApp != null){
            return;
        }
        try {
            _clientApp = PublicClientApplication.builder(_clientId).authority(this.get_Authority()).build();
            Consumer<DeviceCode> deviceCodeConsumer = (DeviceCode deviceCode) -> {
                // Print the login information to the console
                System.out.println(deviceCode.message());
            };
            DeviceCodeFlowParameters codeFlowParameters = DeviceCodeFlowParameters.builder(get_Scopes(), deviceCodeConsumer)
                    .build();
            _clientApp.acquireToken(codeFlowParameters).get();
        }
        catch(MalformedURLException e){
            _clientApp = null;            
        } catch (ExecutionException | InterruptedException e) {
            e.printStackTrace();
        }
    }

    private void fillPropertyValues() {
        Properties prop = new Properties();
        String propFileName = "config.properties";

        try (InputStream inputStream = getClass().getClassLoader().getResourceAsStream(propFileName)) {
            if (inputStream != null) {
                prop.load(inputStream);
                _tenantId = prop.getProperty("UserCredentialTokenGenerator.tenantId");
                _clientId = prop.getProperty("UserCredentialTokenGenerator.clientId");
            }
        } catch (Exception e) {
            System.out.println("Exception: " + e);
        }
    }
}
