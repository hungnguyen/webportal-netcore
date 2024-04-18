import React, { useEffect, useState } from "react";

import {
  TextField,
  Button,
  Grid,
  Card,
  CardHeader,
  CardContent,
  Divider,
  Box,
  FormControlLabel,
  Checkbox,
  IconButton,
  Tooltip,
} from "@material-ui/core";
import useStyles from "../shared/styles";
import { initWebsite, websiteSelector } from "../../redux/website/websiteSlice";
import { updateAsync } from "../../redux/website/websiteAsyncThunk";
import { useSelector, useDispatch } from "react-redux";
import { Save } from "@material-ui/icons";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { Alert } from "@material-ui/lab";
import { useTranslation } from "react-i18next";

export default function Edit() {
  const classes = useStyles();
  const [item, setItem] = useState(initWebsite);
  const website = useSelector(websiteSelector);
  const application = useSelector(applicationSelector);
  const dispatch = useDispatch();
  const [isdone, setIsDone] = useState(false);
  const { t } = useTranslation();

  useEffect(() => {
    setItem(application.website);
  }, [application.website]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleSave = () => {
    if (item.id) {
      dispatch(updateAsync(item));
      setIsDone(true);
    }
  };

  const handleClearCache = () => {
    let itemUpdate = { ...item, isresetcache: true };
    dispatch(updateAsync(itemUpdate));
    setIsDone(true);
  };

  return (
    <>
      <Card className={classes.tablePaper}>
        <CardHeader
          title={t("edit")}
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
            </>
          }
        />
        <Divider />
        <CardContent>
          <form autoComplete="off" className={classes.form}>
            <Grid container spacing={3}>
              {isdone && (
                <Grid item md={12}>
                  <Alert severity="success">
                    {t("website-configuration-is-saved")}
                  </Alert>
                </Grid>
              )}
              <Grid item md={6}>
                <TextField
                  required
                  name="name"
                  label={t("name")}
                  value={item.name}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="smtpserver"
                  label={t("smtp-server")}
                  value={item.smtpserver}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="domain"
                  label={t("domain")}
                  value={item.domain}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="smtpserverport"
                  label={t("smtp-server-port")}
                  value={item.smtpserverport}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="folder"
                  label={t("folder")}
                  value={item.folder}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="smtpusername"
                  label={t("smtp-username")}
                  value={item.smtpusername}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="mobilefolder"
                  label={t("mobile-folder")}
                  value={item.mobilefolder}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  type="password"
                  name="smtpuserpassword"
                  label={t("smtp-user-password")}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="domainalias"
                  label={t("domain-alias")}
                  value={item.domainalias}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="currency"
                  label={t("currency")}
                  value={item.currency}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="uploadfolder"
                  label={t("upload-folder")}
                  value={item.uploadfolder}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="deliveryfee"
                  label={t("deliver-fee")}
                  value={item.deliveryfee}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="fromemail"
                  label={t("from-email")}
                  value={item.fromemail}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="pagedown"
                  label={t("page-down-url")}
                  value={item.pagedown}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="projectname"
                  label={t("project-name")}
                  value={item.projectname}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.smtpssl}
                      onChange={handleChange}
                      name="smtpssl"
                      color="primary"
                    />
                  }
                  label={t("smtp-ssl")}
                />
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.isdown}
                      onChange={handleChange}
                      name="isdown"
                      color="primary"
                    />
                  }
                  label={t("website-is-down")}
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="projectlink"
                  label={t("project-link")}
                  value={item.projectlink}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
            </Grid>
          </form>
        </CardContent>
        <Divider />
        <Box className={classes.formNavigation}>
          <Button
            variant="contained"
            color="primary"
            onClick={handleSave}
            className={classes.saveButton}
          >
            {website.loading ? t("saving") : t("save")}
          </Button>
          <Button
            variant="contained"
            color="secondary"
            onClick={handleClearCache}
            className={classes.saveButton}
          >
            {t("clearCache")}
          </Button>
        </Box>
      </Card>
    </>
  );
}
