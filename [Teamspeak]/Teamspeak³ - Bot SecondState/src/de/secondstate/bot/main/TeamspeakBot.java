package de.secondstate.bot.main;

import com.github.theholywaffle.teamspeak3.TS3Api;
import com.github.theholywaffle.teamspeak3.TS3Config;
import com.github.theholywaffle.teamspeak3.TS3Query;

public class TeamspeakBot {

    public static final TS3Config config = new TS3Config();
    public static final TS3Query query = new TS3Query(config);
    public static final TS3Api api = query.getApi();


    public static void main(String[] args) {

        config.setHost("secondstate");

        query.connect();

        api.login("Query", "eMShEZvs");
        api.setNickname("Welpe ist ein Welpe");
        System.out.println("[BOT-START] Die Query startet....");

    }
}
