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
public class ResponseRestart extends GameResponse {
    private Player player;

    public ResponseRestart() {
        responseCode = Constants.SMSG_RESTART;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player.getID());

        Log.printf("Player with id %d wants a rematch", player.getID());
        
        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}