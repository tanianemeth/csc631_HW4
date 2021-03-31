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
public class ResponseSetPlayer extends GameResponse {
    private int player_id;

    public ResponseSetPlayer() {
        responseCode = Constants.SMSG_SETPLAYER;
    }

    @Override
    public byte[] constructResponseInBytes() {
        GamePacket packet = new GamePacket(responseCode);
        packet.addInt32(player_id);

        return packet.getBytes();
    }

    public void setPlayer(int player_id) {
        this.player_id = player_id;
    }

}