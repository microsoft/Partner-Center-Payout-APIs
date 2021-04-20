import com.google.gson.JsonElement;
import com.google.gson.JsonParser;
import org.jetbrains.annotations.Nullable;

import java.io.*;
import java.net.HttpURLConnection;
import java.net.URL;

public class TransactionHistory {
    static final String Domain = "https://api.partner.microsoft.com/";
    static final String BasePath = "v1.0/payouts/";
    static final String Resource = "transactionhistory";

    public static JsonElement CreateRequest(String accessToken, String filter) throws IOException {
        String createTransactionHistoryRequestUri = String.format("%s%s%s%s", Domain, BasePath, Resource, filter);
        URL url = new URL(createTransactionHistoryRequestUri);
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        connection.setRequestMethod("POST");
        connection.setRequestProperty("Authorization", accessToken);
        connection.setDoOutput(true);
        return getJsonElement(connection);
    }

    @Nullable
    private static JsonElement getJsonElement(HttpURLConnection connection) throws IOException {
        WriteEmptyContent(connection);
        try {
            if (200 == connection.getResponseCode()) {
                BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
                JsonParser parser = new JsonParser();
                return parser.parse(reader);
            }
        } finally {
            connection.disconnect();
        }

        return null;
    }

    public static JsonElement GetRequest(String accessToken, String requestId) throws IOException {
        String createTransactionHistoryRequestUri = String.format("%s%s%s/%s", Domain, BasePath, Resource, requestId);
        URL url = new URL(createTransactionHistoryRequestUri);
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        connection.setRequestMethod("GET");
        connection.setRequestProperty("Authorization", accessToken);
        return getJsonElement(connection);
    }

    public static JsonElement DeleteRequest(String accessToken, String requestId) throws IOException {
        String createTransactionHistoryRequestUri = String.format("%s%s%s/%s", Domain, BasePath, Resource, requestId);
        URL url = new URL(createTransactionHistoryRequestUri);
        HttpURLConnection connection = (HttpURLConnection) url.openConnection();
        connection.setRequestMethod("DELETE");
        connection.setRequestProperty("Authorization", accessToken);
        return getJsonElement(connection);
    }

    private static void WriteEmptyContent(HttpURLConnection connection) throws IOException {
        OutputStream outputStream = connection.getOutputStream();
        outputStream.flush();
        outputStream.close();
    }
}
