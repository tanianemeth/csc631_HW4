package metadata;

/**
 * The Constants class stores important variables as constants for later use.
 */
public class Constants {
    // Constants
	final public static String CLIENT_VERSION = "1.00";
	final public static String REMOTE_HOST = "localhost";
    final public static int REMOTE_PORT = 9252;
    final public static int TIMEOUT_SECONDS = 90;
    
    // Request (1xx) + Response (2xx)
	final public static short CMSG_JOIN = 101;
	final public static short SMSG_JOIN = 201;
	final public static short CMSG_LEAVE = 102;
	final public static short SMSG_LEAVE = 202;
	final public static short CMSG_SETNAME = 103;
	final public static short SMSG_SETNAME = 203;
	final public static short CMSG_READY = 104;
	final public static short SMSG_READY = 204;
	final public static short CMSG_MOVE = 105;
	final public static short SMSG_MOVE = 205;
	final public static short CMSG_INTERACT = 106;
	final public static short SMSG_INTERACT = 206;
	final public static short CMSG_WIN = 107;
	final public static short SMSG_WIN = 207;
	final public static short CMSG_RESTART = 108;
	final public static short SMSG_RESTART = 208;
	final public static short CMSG_STARTINGPLAYER = 109;
	final public static short SMSG_STARTINGPLAYER = 209;
	final public static short CMSG_SCORE = 110;
	final public static short SMSG_SCORE = 210;
	final public static short CMSG_HEARTBEAT = 111;

	final public static int USER_ID = -1;
	final public static int OP_ID = -1;
}
