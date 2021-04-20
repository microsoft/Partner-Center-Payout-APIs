import com.google.gson.JsonElement;

import java.io.IOException;
import java.util.concurrent.ExecutionException;

public class Program {
    public static void main(String args[]){
        try {
            UserCredentialTokenGenerator generator = new UserCredentialTokenGenerator();
            String token = "Bearer " + generator.GetToken();
            JsonElement txRequest = TransactionHistory.CreateRequest(token, "");
            System.out.println(txRequest.toString());
        } catch (IOException ioException) {
            ioException.printStackTrace();
        } catch (ExecutionException e) {
            e.printStackTrace();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
