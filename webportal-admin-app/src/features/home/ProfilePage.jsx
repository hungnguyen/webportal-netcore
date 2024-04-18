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
  Typography,
  IconButton,
  Tooltip,
} from "@material-ui/core";
import useStyles from "../shared/styles";
import { initAppUser, appuserSelector } from "../../redux/appuser/appuserSlice";
import { updateAsync } from "../../redux/appuser/appuserAsyncThunk";
import { getProfileAsync } from "../../redux/account/accountAsyncThunk";
import { useSelector, useDispatch } from "react-redux";
import Editor from "../shared/Editor";
import { Save } from "@material-ui/icons";

import { getDateOnly } from "../shared/utils";

import { applicationSelector } from "../../redux/application/applicationSlice";
import { accountSelector } from "../../redux/account/accountSlice";
import { Alert } from "@material-ui/lab";
import ValidatorSummary from "../shared/ValidatorSummary";
import { useTranslation } from "react-i18next";

export default function ProfilePage() {
  const classes = useStyles();
  const [item, setItem] = useState(initAppUser);
  const appuser = useSelector(appuserSelector);

  const application = useSelector(applicationSelector);
  const account = useSelector(accountSelector);
  const dispatch = useDispatch();
  const [imageSrc, setImageSrc] = useState("");
  const [imageFile, setImageFile] = useState(null);
  const [isdone, setIsDone] = useState(false);
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (account.profile.id === undefined) {
      dispatch(getProfileAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setItem(account.profile);
    if (account.profile.image !== "") {
      setImageSrc(`${application.imageBaseAddress}/${account.profile.image}`);
    }
  }, [account.profile, application.imageBaseAddress]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleSave = () => {
    if (!isValid()) return;

    if (item.id) {
      dispatch(
        updateAsync({
          item,
          imageData: getFormData(),
        })
      );
      setIsDone(true);
    }
  };

  const handleImageChange = (e) => {
    // Assuming only image
    var file = e.target.files[0];
    var reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onloadend = function (e) {
      setImageSrc(reader.result);
    };
    setImageFile(file);
  };

  const getFormData = () => {
    if (imageFile !== null) {
      let formData = new FormData();
      formData.append("file", imageFile, imageFile.name);
      return formData;
    }
    return null;
  };

  const isValid = () => {
    let arr = [];
    if (item.username === "") {
      arr = arr.concat(t("field-cannot-empty", { fieldName: t("user-name") }));
    }

    //return
    if (arr.length > 0) {
      setErrors(arr);
      return false;
    }
    return true;
  };

  return (
    <>
      <Typography variant="h4" gutterBottom>
        {t("profile")}
      </Typography>
      <Card className={classes.tablePaper}>
        <CardHeader
          title={t("update")}
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
              <Grid item md={12}>
                <ValidatorSummary errors={errors} />
              </Grid>
              {isdone && (
                <Grid item md={12}>
                  <Alert severity="success">{t("user-profile-is-saved")}</Alert>
                </Grid>
              )}
              <Grid item md={12}>
                <Typography>{t("image")}:</Typography>
                <Grid item md={6}>
                  {imageSrc !== "" && (
                    <img src={imageSrc} alt="" className={classes.image} />
                  )}
                </Grid>
              </Grid>
              <Grid item md={12}>
                <input
                  accept="image/*"
                  className={classes.hidden}
                  id="contained-button-file"
                  multiple
                  type="file"
                  onChange={handleImageChange}
                />
                <label htmlFor="contained-button-file">
                  <Button variant="outlined" color="primary" component="span">
                    {t("browse-image")}
                  </Button>
                </label>
              </Grid>
              <Grid item md={6}>
                <TextField
                  required
                  name="username"
                  label={t("user-name")}
                  value={item.username}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="fullname"
                  label={t("full-name")}
                  value={item.fullname}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="newpassword"
                  label={t("password")}
                  onChange={handleChange}
                  variant="outlined"
                  type="password"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="phonenumber"
                  label={t("phone-number")}
                  value={item.phonenumber}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="email"
                  label={t("email")}
                  value={item.email}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="birthday"
                  label={t("birthday")}
                  value={getDateOnly(item.birthday)}
                  onChange={handleChange}
                  variant="outlined"
                  type="date"
                />
              </Grid>

              <Grid item md={12}>
                <Editor
                  name="note"
                  label={t("note")}
                  data={item.note}
                  onChange={handleChange}
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
            {appuser.loading ? t("saving") : t("save")}
          </Button>
        </Box>
      </Card>
    </>
  );
}
