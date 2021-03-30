package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseStartingPlayer;
import core.NetworkManager;

public class RequestStartingPlayer extends GameRequest {

    // Responses
    private ResponseStartingPlayer responseStartingPlayer;

    public RequestStartingPlayer() {
        responses.add(responseStartingPlayer = new ResponseStartingPlayer());
    }

    @Override
    public void parse() throws IOException {
      
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseStartingPlayer.setPlayer(player);

        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseStartingPlayer);
    }
}