package de.hamburg.ts3bot.load;

import com.github.theholywaffle.teamspeak3.TS3Api;
import com.github.theholywaffle.teamspeak3.TS3Config;
import com.github.theholywaffle.teamspeak3.TS3Query;

public class BotLoad {


    public static final TS3Query query = new TS3Query();
    public static final TS3Config config = new TS3Config();
    public static final TS3Api api = query.getApi();

    config.setHost("77.77.77.77");
    query.connect();

    api.login("serveradmin", "serveradminpassword");
    api.selectVirtualServerById(1);
    api.setNickname("PutPutBot");
    api.sendChannelMessage("PutPutBot is online!");
}
