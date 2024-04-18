import { Box, TextField, Grid } from "@material-ui/core";
import React, { useState, useEffect, useCallback } from "react";
import useStyles from "../shared/styles";
import { HubConnectionBuilder } from "@microsoft/signalr";
import axios from "axios";
import { useSelector, useDispatch } from "react-redux";
import { accountSelector } from "../../redux/account/accountSlice";
import { chatSelector, addMessage, updateTo } from "../../redux/chat/chatSlice";

export default function ChatBox() {
  const [connection, setConnection] = useState(null);
  const classes = useStyles();
  const [message, setMessage] = useState("");
  const account = useSelector(accountSelector);
  const chat = useSelector(chatSelector);
  const dispatch = useDispatch();
  const [currentUser, setCurrentUser] = useState(account.profile.username);

  useEffect(() => {
    if (account.profile.username !== "") {
      setCurrentUser(account.profile.username);
    }
  }, [account]);

  useEffect(() => {
    const connect = new HubConnectionBuilder()
      .withUrl(`${axios.defaults.baseURL.replace("api", "chatHub")}`)
      .withAutomaticReconnect()
      .build();

    setConnection(connect);
  }, []);

  const handleReceiveMessage = useCallback((from, to, message) => {
    if (to === currentUser) {
      dispatch(updateTo(from));
    }

    dispatch(
      addMessage({
        from,
        to,
        message,
      })
    );
  },[currentUser, dispatch]);

  useEffect(() => {
    if (connection && currentUser !== "") {
      connection
        .start()
        .then(() => {
          connection.on("ReceiveMessage", handleReceiveMessage);
        })
        .catch((error) => console.log(error));
    }
  }, [connection, currentUser, handleReceiveMessage]);

  const sendMessage = async () => {
    if (connection)
      await connection.send(
        "SendMessage",
        currentUser,
        chat.currentTo,
        message
      );
    setMessage("");
  };

  const handleSend = (e) => {
    if (e.key === "Enter") {
      sendMessage();
    }
  };

  const handleChange = (e) => {
    setMessage(e.target.value);
  };

  return (
    <>
      <Box className={classes.messageBox}>
        <Grid container>
          {chat.messages
            .filter(
              (i) =>
                (i.to === currentUser && i.from === chat.currentTo) ||
                (i.from === currentUser && i.to === chat.currentTo)
            )
            .map((item) => (
              <Grid item xs={12}>
                <Box
                  className={
                    item.from === currentUser
                      ? classes.messageOut
                      : classes.messageIn
                  }
                >
                  {item.message}
                </Box>
              </Grid>
            ))}
        </Grid>
      </Box>
      <TextField
        variant="outlined"
        label="Message"
        fullWidth
        value={message}
        onKeyDown={handleSend}
        onChange={handleChange}
      ></TextField>
    </>
  );
}
