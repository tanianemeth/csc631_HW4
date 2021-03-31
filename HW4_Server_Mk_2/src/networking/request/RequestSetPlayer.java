package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseSetPlayer;
import networking.response.ResponseWin;
import utility.DataReader;
import core.NetworkManager;

public class RequestSetPlayer extends GameRequest {

    // Responses
    private ResponseSetPlayer responseSetPlayer;
    private int player_id;

    public RequestSetPlayer() {
        responses.add(responseSetPlayer = new ResponseSetPlayer());
    }

    @Override
    public void parse() throws IOException {
        player_id = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();

        responseSetPlayer.setPlayer(player_id);
        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseSetPlayer);
    }
}