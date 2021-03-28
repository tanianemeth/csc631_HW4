package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseWin;
import utility.DataReader;
import core.NetworkManager;

public class RequestWin extends GameRequest {
    
    // Responses
    private ResponseWin responseWin;

    public RequestWin() {
        responses.add(responseWin = new ResponseWin());
    }

    @Override
    public void parse() throws IOException {
     
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseWin.setPlayer(player);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseWin);
    }
}