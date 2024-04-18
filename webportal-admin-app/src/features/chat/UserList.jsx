import React, { useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { appuserSelector } from "../../redux/appuser/appuserSlice";
import { accountSelector } from "../../redux/account/accountSlice";
import { getAllAsync } from "../../redux/appuser/appuserAsyncThunk";
import { selectTo, chatSelector } from "../../redux/chat/chatSlice";
import { makeStyles } from "@material-ui/core/styles";
import {
  List,
  ListItem,
  ListItemAvatar,
  Avatar,
  ListItemText,
  ListItemSecondaryAction,
  Badge,
} from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
  root: {
    width: "100%",
    maxWidth: 360,
    backgroundColor: theme.palette.background.paper,
  },
}));

export default function UserList() {
  const classes = useStyles();
  const application = useSelector(applicationSelector);
  const appuser = useSelector(appuserSelector);
  const account = useSelector(accountSelector);
  const chat = useSelector(chatSelector);
  const dispatch = useDispatch();

  useEffect(() => {
    dispatch(getAllAsync());
    var interval = setInterval(() => {
      dispatch(getAllAsync());
    }, 5000);
    return () => {
      clearInterval(interval);
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleSelectUser = (username) => {
    dispatch(selectTo(username));
  };
  return (
    <>
      <List dense className={classes.root}>
        {appuser.list
          .filter((item) => item.username !== account.profile.username)
          .map((item) => {
            const labelId = `checkbox-list-secondary-label-${item.id}`;
            return (
              <ListItem
                key={item.id}
                button
                onClick={() => handleSelectUser(item.username)}
                selected={item.username === chat.currentTo}
              >
                <ListItemAvatar>
                  <Avatar
                    alt={`${item.username}`}
                    src={`${application.imageBaseAddress}${
                      item.image ?? "noavatar.png"
                    }`}
                  />
                </ListItemAvatar>
                <ListItemText
                  id={labelId}
                  primary={`${item.fullname} (${item.username})`}
                />
                <ListItemSecondaryAction>
                  {item.isonline && (
                    <Badge color="primary" variant="dot"></Badge>
                  )}
                </ListItemSecondaryAction>
              </ListItem>
            );
          })}
      </List>
    </>
  );
}
