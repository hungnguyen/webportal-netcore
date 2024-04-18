import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import {
  TextField,
  Button,
  Grid,
  Typography,
  FormControl,
  InputLabel,
  Select,
  MenuItem,
} from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initCustomer,
  unselect,
  customerSelector,
} from "../../redux/customer/customerSlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
} from "../../redux/customer/customerAsyncThunk";
import { useSelector, useDispatch } from "react-redux";
import { enumSelector } from "../../redux/enum/enumSlice";
import { equals, getDateOnly } from "../shared/utils";
import { applicationSelector } from "../../redux/application/applicationSlice";
import ValidatorSummary from "../shared/ValidatorSummary";
import { useTranslation } from "react-i18next";
import EditFormContainer from "../shared/EditFormContainer";

export default function Edit() {
  const history = useHistory();
  const classes = useStyles();
  const { id } = useParams();
  const [item, setItem] = useState(initCustomer);
  const customer = useSelector(customerSelector);
  const application = useSelector(applicationSelector);

  const dispatch = useDispatch();
  const enums = useSelector(enumSelector);
  const [imageSrc, setImageSrc] = useState("");
  const [imageFile, setImageFile] = useState(null);
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (id) {
      if (!equals(customer.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setItem(customer.item);
    if (customer.item.image !== "") {
      setImageSrc(`${application.imageBaseAddress}/${customer.item.image}`);
    }
  }, [customer.item, application.imageBaseAddress]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/customer");
  };

  const handleSave = () => {
    if (!isValid()) return;

    if (item.id) {
      dispatch(updateAsync({ item, imageData: getFormData() }));
    } else {
      dispatch(
        createAsync({
          item: {
            ...item,
            websiteid: application.website.id,
            datecreated: new Date(),
          },
          imageData: getFormData(),
        })
      );
    }
    dispatch(unselect());
    history.push("/customer");
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
    let formData = new FormData();
    if (imageFile !== null) {
      formData.append("file", imageFile, imageFile.name);
    }
    return formData;
  };

  const isValid = () => {
    let arr = [];
    if (item.fullname === "") {
      arr = arr.concat(t("field-cannot-empty", { fieldName: t("full-name") }));
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
      <EditFormContainer
        handleCancel={handleCancel}
        handleSave={handleSave}
        loading={customer.loading}
      >
        <form autoComplete="off" className={classes.form}>
          <Grid container spacing={3}>
            <ValidatorSummary errors={errors} />
            <Grid item md={12}>
              <Typography>{t("image")}:</Typography>
              <Grid md={6}>
                {imageSrc !== "" && <img src={imageSrc} alt="" width="100%" />}
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
                name="fullname"
                label={t("full-name")}
                value={item.fullname}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>
            <Grid item md={6}>
              <TextField
                name="birthday"
                label={t("birthday")}
                type="date"
                value={getDateOnly(item.birthday)}
                onChange={handleChange}
                variant="outlined"
                InputLabelProps={{
                  shrink: true,
                }}
              />
            </Grid>
            <Grid item md={6}>
              <TextField
                name="email"
                label={t("email")}
                type="email"
                value={item.email}
                onChange={handleChange}
                variant="outlined"
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
                name="idcard"
                label={t("id-card")}
                value={item.idcard}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>
            <Grid item md={6}>
              <TextField
                name="address"
                label={t("address")}
                value={item.address}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>
            <Grid item md={6}>
              <FormControl className={classes.formControl} variant="outlined">
                <InputLabel id="status-label">{t("status")}</InputLabel>
                <Select
                  labelId="status-label"
                  id="status"
                  name="status"
                  value={item.status}
                  onChange={handleChange}
                  label={t("status")}
                >
                  <MenuItem key={0} value={0}>
                    <em>{t("none")}</em>
                  </MenuItem>
                  {enums.status.map((i) => (
                    <MenuItem key={i.value} value={i.key}>
                      {t(i.key)}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
            <Grid item md={6}>
              <FormControl className={classes.formControl} variant="outlined">
                <InputLabel id="gender-label">{t("gender")}</InputLabel>
                <Select
                  labelId="gender-label"
                  id="gender"
                  name="gender"
                  value={item.gender}
                  onChange={handleChange}
                  label={t("gender")}
                >
                  <MenuItem key={0} value={0}>
                    <em>{t("none")}</em>
                  </MenuItem>
                  {enums.gender.map((i) => (
                    <MenuItem key={i.value} value={i.key}>
                      {t(i.key)}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Grid>
          </Grid>
        </form>
      </EditFormContainer>
    </>
  );
}
