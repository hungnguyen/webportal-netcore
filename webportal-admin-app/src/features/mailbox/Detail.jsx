import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import {
  Button,
  Grid,
  Card,
  CardHeader,
  CardContent,
  Divider,
  IconButton,
  Typography,
  Box,
  Tooltip,
} from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initMailBox,
  unselect,
  mailboxSelector,
} from "../../redux/mailbox/mailboxSlice";
import { getByIdAsync } from "../../redux/mailbox/mailboxAsyncThunk";
import { useSelector, useDispatch } from "react-redux";
import { Close } from "@material-ui/icons";
import { equals, renderHTML } from "../shared/utils";
import { useTranslation } from "react-i18next";

export default function Detail() {
  const history = useHistory();
  const classes = useStyles();
  const { id } = useParams();
  const [item, setItem] = useState(initMailBox);
  const mailbox = useSelector(mailboxSelector);
  const dispatch = useDispatch();
  const { t } = useTranslation();

  useEffect(() => {
    if (id) {
      if (!equals(mailbox.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setItem(mailbox.item);
  }, [mailbox.item]);

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/mailbox");
  };

  return (
    <>
      <Button variant="outlined" onClick={handleCancel}>
        {t("back-to-list")}
      </Button>
      <Card className={classes.tablePaper}>
        <CardHeader
          title={t("detail")}
          action={
            <>
              <Tooltip title={t("close")}>
                <IconButton aria-label="settings" onClick={handleCancel}>
                  <Close />
                </IconButton>
              </Tooltip>
            </>
          }
        />
        <Divider />
        <CardContent>
          <form autoComplete="off" className={classes.form}>
            <Grid container spacing={3}>
              <Grid item md={12}>
                <Typography variant="h6">{item.subject}</Typography>
              </Grid>
              <Grid item container md={12}>
                <Grid item md={1}>
                  <Typography variant="subtitle2">
                    {t("from-email")}:
                  </Typography>
                </Grid>
                <Grid item md={9}>
                  <Typography>{item.fromemail}</Typography>
                </Grid>
              </Grid>
              <Grid item container md={12}>
                <Grid item md={1}>
                  <Typography variant="subtitle2">{t("to-email")}:</Typography>
                </Grid>
                <Grid item md={9}>
                  <Typography>{item.toemail}</Typography>
                </Grid>
              </Grid>
              <Grid item md={12}>
                <Divider />
              </Grid>
              <Grid item md={12}>
                {renderHTML(item.body)}
              </Grid>
            </Grid>
          </form>
        </CardContent>
        <Divider />
        <Box className={classes.formNavigation}>
          <Button variant="contained" onClick={handleCancel}>
            {t("close")}
          </Button>
        </Box>
      </Card>
    </>
  );
}
