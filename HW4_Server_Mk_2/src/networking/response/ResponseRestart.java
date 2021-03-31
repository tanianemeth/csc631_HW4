package networking.response;

// Other Imports
import core.GameServer;
import metadata.Constants;
import model.Player;
import utility.GamePacket;
import utility.Log;

import java.util.List;

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

        GameServer gs = GameServer.getInstance();
        List<Player> activePlayers = gs.getActivePlayers();

        for(Player p : activePlayers) {
            if(p.getID() == player.getID())
                gs.removeActivePlayer(p.getID());
        }

        Log.printf("Player with id %d has left.", player.getID());

        return packet.getBytes();
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}