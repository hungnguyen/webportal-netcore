import React from "react";
import {
  Button,
  Card,
  CardHeader,
  Tooltip,
  IconButton,
  Divider,
  CardContent,
  Box,
} from "@material-ui/core";
import { Save, Close } from "@material-ui/icons";
import { useTranslation } from "react-i18next";
import useStyles from "./styles";

export default function EditFormContainer({
  children,
  handleCancel,
  handleSave,
  loading,
  title,
}) {
  const { t } = useTranslation();
  const classes = useStyles();
  return (
    <>
      <Button variant="outlined" onClick={handleCancel}>
        {t("back-to-list")}
      </Button>
      <Card className={classes.tablePaper}>
        <CardHeader
          title={title ? title : t("add-edit")}
          action={
            <>
              <Tooltip title={t("save")}>
                <IconButton
                  aria-label="settings"
                  onClick={handleSave}
                  color="primary"
                >
                  <Save />
                </IconButton>
              </Tooltip>
              <Tooltip title={t("close")}>
                <IconButton aria-label="settings" onClick={handleCancel}>
                  <Close />
                </IconButton>
              </Tooltip>
            </>
          }
        />
        <Divider />
        <CardContent>{children}</CardContent>
        <Divider />
        <Box className={classes.formNavigation}>
          <Button
            variant="contained"
            color="primary"
            onClick={handleSave}
            className={classes.saveButton}
          >
            {loading ? t("saving") : t("save")}
          </Button>
          <Button variant="contained" onClick={handleCancel}>
            {t("cancel")}
          </Button>
        </Box>
      </Card>
    </>
  );
}
