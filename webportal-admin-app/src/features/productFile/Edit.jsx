import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import { TextField, Button, Grid, Typography } from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initProductFile,
  unselect,
  productFileSelector,
} from "../../redux/productFile/productFileSlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
} from "../../redux/productFile/productFileAsyncThunk";

import { useSelector, useDispatch } from "react-redux";
import Editor from "../shared/Editor";
import { accountSelector } from "../../redux/account/accountSlice";
import { applicationSelector } from "../../redux/application/applicationSlice";
import ValidatorSummary from "../shared/ValidatorSummary";
import { useTranslation } from "react-i18next";
import EditFormContainer from "../shared/EditFormContainer";
import { equals } from "../shared/utils";

export default function Edit() {
  const history = useHistory();
  const classes = useStyles();
  const { id, productid, type } = useParams();
  const [item, setItem] = useState(initProductFile);
  const productFile = useSelector(productFileSelector);

  const account = useSelector(accountSelector);
  const application = useSelector(applicationSelector);
  const dispatch = useDispatch();
  const [imageSrc, setImageSrc] = useState("");
  const [imageFile, setImageFile] = useState(null);
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (id) {
      if (!equals(productFile.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setItem(productFile.item);
    if (productFile.item.image !== "") {
      setImageSrc(
        `${application.imageBaseAddress}/${productFile.item.filename}`
      );
    }
  }, [productFile.item, application.imageBaseAddress]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleCancel = () => {
    dispatch(unselect());
    history.push(`/product-file/${type}/${productid}`);
  };

  const handleSave = () => {
    if (!isValid()) return;

    if (item.id) {
      dispatch(
        updateAsync({
          item: {
            ...item,
            updatedby: account.profile.username,
            dateupdated: new Date(),
          },
          imageData: getFormData(),
        })
      );
    } else {
      dispatch(
        createAsync({
          item: {
            ...item,
            productid,
            createdby: account.profile.username,
            datecreated: new Date(),
            updatedby: account.profile.username,
            dateupdated: new Date(),
          },
          imageData: getFormData(),
        })
      );
    }
    dispatch(unselect());
    history.push(`/product-file/${type}/${productid}`);
  };

  const isValid = () => {
    let arr = [];
    if (item.name === "") {
      arr = arr.concat(t("field-cannot-empty", { fieldName: t("name") }));
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

  return (
    <>
      <EditFormContainer
        handleCancel={handleCancel}
        handleSave={handleSave}
        loading={productFile.loading}
      >
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
                name="name"
                label={t("name")}
                value={item.name}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>
            <Grid item md={6}>
              <TextField
                name="ordernumber"
                label={t("order-number")}
                value={item.ordernumber}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>
            <Grid item md={6}>
              <TextField
                name="link"
                label={t("link")}
                value={item.link}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>

            <Grid item md={12}>
              {(item.detail !== "" || id === undefined) && (
                <Editor
                  name="detail"
                  label={t("detail")}
                  data={item.detail}
                  onChange={handleChange}
                />
              )}
            </Grid>
          </Grid>
        </form>
      </EditFormContainer>
    </>
  );
}
