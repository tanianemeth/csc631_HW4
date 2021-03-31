package networking.request;

// Java Imports
import java.io.IOException;

// Other Imports
import model.Player;
import networking.response.ResponseScore;
import utility.DataReader;
import core.NetworkManager;

public class RequestScore extends GameRequest {
    private int score;
    // Responses
    private ResponseScore responseScore;

    public RequestScore() { responses.add(responseScore = new ResponseScore()); }

    @Override
    public void parse() throws IOException {
        score = DataReader.readInt(dataInput);
    }

    @Override
    public void doBusiness() throws Exception {
        Player player = client.getPlayer();
        responseScore.setPlayer(player);
        responseScore.setScore(score);


        NetworkManager.addResponseForAllOnlinePlayers(player.getID(), responseScore);
    }
}