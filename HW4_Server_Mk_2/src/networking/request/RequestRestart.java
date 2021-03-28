package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseRestart;
import core.NetworkManager;

public class RequestRestart extends GameRequest {

    // Responses
    private ResponseRestart responseRestart;

    public RequestRestart() {
        responses.add(responseRestart = new ResponseRestart());
    }

    @Override
    public void parse() throws IOException {
    
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseRestart.setPlayer(player);

        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseRestart);
    }
}