package networking.response;

// Other Imports
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;
/**
 * The ResponseLogin class contains information about the authentication
 * process.
 */
public class ResponseScore extends GameResponse {
    private Player player;
    private int score;

    public ResponseScore() {
        responseCode = Constants.SMSG_SCORE;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());
        packet.addInt32(score);
      

        Log.printf("Player with id %d has %d wins", player.getID(), score);
 
        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }

    public void setScore(int score)
    {
        this.score = score;
    }

}