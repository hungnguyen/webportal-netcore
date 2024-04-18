import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import {
  TextField,
  Button,
  Grid,
  Typography,
  FormControlLabel,
  Checkbox,
} from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initAppUser,
  unselect,
  appuserSelector,
} from "../../redux/appuser/appuserSlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
} from "../../redux/appuser/appuserAsyncThunk";

import { useSelector, useDispatch } from "react-redux";
import Editor from "../shared/Editor";
import { approleSelector } from "../../redux/approle/approleSlice";
import { getAllAsync } from "../../redux/approle/approleAsyncThunk";
import { equals, getDateOnly } from "../shared/utils";
import { getByUserIdAsync } from "../../redux/appuserrole/appuserroleAsyncThunk";
import {
  appuserroleSelector,
  clearAll,
} from "../../redux/appuserrole/appuserroleSlice";
import { applicationSelector } from "../../redux/application/applicationSlice";
import ValidatorSummary from "../shared/ValidatorSummary";
import { useTranslation } from "react-i18next";
import EditFormContainer from "../shared/EditFormContainer";

export default function Edit() {
  const history = useHistory();
  const classes = useStyles();
  const { id } = useParams();
  const [item, setItem] = useState(initAppUser);
  const appuser = useSelector(appuserSelector);
  const approle = useSelector(approleSelector);
  const appuserrole = useSelector(appuserroleSelector);
  const application = useSelector(applicationSelector);
  const dispatch = useDispatch();
  const [imageSrc, setImageSrc] = useState("");
  const [imageFile, setImageFile] = useState(null);
  const [selectedRoles, setSelectedRoles] = useState([]);
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (approle.list.length === 0) {
      dispatch(getAllAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (id) {
      if (!equals(appuser.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
      dispatch(getByUserIdAsync(id));
    } else {
      dispatch(clearAll());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setSelectedRoles(appuserrole.list);
  }, [appuserrole.list]);

  useEffect(() => {
    setItem(appuser.item);
    if (appuser.item.image !== "") {
      setImageSrc(`${application.imageBaseAddress}/${appuser.item.image}`);
    }
  }, [appuser.item, application.imageBaseAddress]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/appuser");
  };

  const handleSave = () => {
    if (!isValid()) return;

    if (item.id) {
      dispatch(
        updateAsync({
          item,
          imageData: getFormData(),
          inRole: selectedRoles,
        })
      );
    } else {
      dispatch(
        createAsync({
          item,
          imageData: getFormData(),
          inRole: selectedRoles,
        })
      );
    }
    dispatch(unselect());
    history.push("/appuser");
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

  const handleChangeRole = (value) => {
    const currentIndex = selectedRoles.indexOf(value);
    const newChecked = [...selectedRoles];

    if (currentIndex === -1) {
      newChecked.push(value);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setSelectedRoles(newChecked);
  };

  return (
    <>
      <EditFormContainer
        handleCancel={handleCancel}
        handleSave={handleSave}
        loading={appuser.loading}
      >
        {(equals(item.id, id) || id === undefined) && !appuser.loading && (
          <form autoComplete="off" className={classes.form}>
            <Grid container spacing={3}>
              <ValidatorSummary errors={errors} />
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
                  label="Email"
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
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
              </Grid>

              <Grid item md={12}>
                {
                  <Editor
                    name="note"
                    label={t("note")}
                    data={item.note}
                    onChange={handleChange}
                  />
                }
              </Grid>
              <Grid item md={12}>
                <Typography>{t("role")}:</Typography>
              </Grid>
              {approle.list.map((i) => (
                <Grid item md={3}>
                  <FormControlLabel
                    control={
                      <Checkbox
                        checked={selectedRoles.includes(i.name)}
                        onChange={() => handleChangeRole(i.name)}
                        name="inrole"
                        color="primary"
                        value={i.name}
                      />
                    }
                    label={i.name}
                  />
                </Grid>
              ))}
            </Grid>
          </form>
        )}
      </EditFormContainer>
    </>
  );
}
