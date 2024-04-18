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
  FormControlLabel,
  Checkbox,
} from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initCategory,
  unselect,
  categorySelector,
} from "../../redux/category/categorySlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
  getAllAsync,
} from "../../redux/category/categoryAsyncThunk";

import { enumSelector } from "../../redux/enum/enumSlice";
import { useSelector, useDispatch } from "react-redux";
import Editor from "../shared/Editor";
import { accountSelector } from "../../redux/account/accountSlice";
import { productTypeSelector } from "../../redux/productType/productTypeSlice";
import { applicationSelector } from "../../redux/application/applicationSlice";
import { getUrlName } from "../shared/stringUtils";
import ValidatorSummary from "../shared/ValidatorSummary";
import { useTranslation } from "react-i18next";
import EditFormContainer from "../shared/EditFormContainer";
import { equals } from "../shared/utils";
import Loading from "../shared/Loading";

export default function Edit() {
  const history = useHistory();
  const classes = useStyles();
  const { id } = useParams();
  const [item, setItem] = useState(initCategory);
  const category = useSelector(categorySelector);
  const enums = useSelector(enumSelector);
  const account = useSelector(accountSelector);
  const productType = useSelector(productTypeSelector);
  const application = useSelector(applicationSelector);

  const dispatch = useDispatch();
  const [imageSrc, setImageSrc] = useState("");
  const [iconSrc, setIconSrc] = useState("");
  const [imageFile, setImageFile] = useState(null);
  const [iconFile, setIconFile] = useState(null);
  const [catsByType, setCatsByType] = useState([]);
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (category.list.length === 0) {
      dispatch(getAllAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (id) {
      if (!equals(category.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (item.typecode !== 0) {
      setCatsByType(category.list.filter((i) => i.typecode === item.typecode));
    }
  }, [category.list, item.typecode]);

  useEffect(() => {
    setItem(category.item);
    if (category.item.image !== "") {
      setImageSrc(`${application.imageBaseAddress}/${category.item.image}`);
    }
    if (category.item.icon !== "") {
      setIconSrc(`${application.imageBaseAddress}/${category.item.icon}`);
    }
  }, [category.item, application.imageBaseAddress]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({
      ...item,
      [name]: ["checkbox"].includes(type) ? checked : value,
      urlname: name === "name" ? getUrlName(value) : item.urlname,
    });
  };

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/category");
  };

  const handleSave = () => {
    if (!isValid()) return;

    if (item.id) {
      dispatch(
        updateAsync({
          item: {
            ...item,
            urlname: item.urlname === "" ? getUrlName(item.name) : item.urlname,
            updateby: account.profile.username,
            dateupdated: new Date(),
          },
          imageData: getImageData(),
          iconData: getIconData(),
        })
      );
    } else {
      dispatch(
        createAsync({
          item: {
            ...item,
            urlname: item.urlname === "" ? getUrlName(item.name) : item.urlname,
            createdby: account.profile.username,
            datecreated: new Date(),
            updatedby: account.profile.username,
            dateupdated: new Date(),
            websiteid: application.website.id,
            languageid: application.languageid,
          },
          imageData: getImageData(),
          iconData: getIconData(),
        })
      );
    }
    dispatch(unselect());
    history.push("/category");
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
  const handleIconChange = (e) => {
    // Assuming only image
    var file = e.target.files[0];
    var reader = new FileReader();
    reader.readAsDataURL(file);

    reader.onloadend = function (e) {
      setIconSrc(reader.result);
    };
    setIconFile(file);
  };

  const getImageData = () => {
    if (imageFile !== null) {
      let formData = new FormData();
      formData.append("file", imageFile, imageFile.name);
      return formData;
    }
    return null;
  };
  const getIconData = () => {
    if (iconFile !== null) {
      let formData = new FormData();
      formData.append("file", iconFile, iconFile.name);
      return formData;
    }
    return null;
  };

  const buildCatTree = (all, parentid = 0, prefix = "") => {
    let list = all.filter((i) => i.parentid === parentid);
    let result = [];

    list.map((i) => {
      result.push({ id: i.id, name: `${prefix}${i.name}` });
      let child = buildCatTree(all, i.id, `${prefix}---`);
      if (child.length > 0) result = result.concat(child);
      return null;
    });
    return result;
  };

  const isValid = () => {
    let arr = [];
    if (item.name === "") {
      arr = arr.concat("Name cannot empty");
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
        loading={category.loading}
      >
        {category.loading && (<Loading />)}
        {(equals(item.id, id) || id === undefined) && !category.loading && (
          <form autoComplete="off" className={classes.form}>
            <Grid container spacing={3}>
              <ValidatorSummary errors={errors} />
              <Grid item md={6}>
                <Typography>{t("image")}:</Typography>
                <Grid item md={6}>
                  {imageSrc !== "" && (
                    <img src={imageSrc} alt="" width="100%" />
                  )}
                </Grid>
              </Grid>
              <Grid item md={6}>
                <Typography>{t("icon")}:</Typography>
                <Grid item md={6}>
                  {iconSrc !== "" && <img src={iconSrc} alt="" width="100%" />}
                </Grid>
              </Grid>
              <Grid item md={6}>
                <input
                  accept="image/*"
                  className={classes.hidden}
                  id="image"
                  multiple
                  type="file"
                  onChange={handleImageChange}
                />
                <label htmlFor="image">
                  <Button variant="outlined" color="primary" component="span">
                    {t("browse-image")}
                  </Button>
                </label>
              </Grid>
              <Grid item md={6}>
                <input
                  accept="image/*"
                  className={classes.hidden}
                  id="icon"
                  multiple
                  type="file"
                  onChange={handleIconChange}
                />
                <label htmlFor="icon">
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
                  name="urlname"
                  label={t("url-name")}
                  value={item.urlname}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <FormControl className={classes.formControl} variant="outlined">
                  <InputLabel id="type-label">{t("type")}</InputLabel>
                  <Select
                    labelId="type-label"
                    id="typecode"
                    name="typecode"
                    value={item.typecode}
                    onChange={handleChange}
                    label={t("type")}
                  >
                    <MenuItem key={0} value={0}>
                      <em>{t("none")}</em>
                    </MenuItem>
                    {productType.list.map((i) => (
                      <MenuItem key={i.id} value={i.code}>
                        {i.name}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="displaytype"
                  label={t("display-type")}
                  value={item.displaytype}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>
              <Grid item md={6}>
                <FormControl className={classes.formControl} variant="outlined">
                  <InputLabel id="parent-label">{t("parent")}</InputLabel>
                  <Select
                    labelId="parent-label"
                    id="parentid"
                    name="parentid"
                    value={item.parentid}
                    onChange={handleChange}
                    label={t("parent")}
                  >
                    <MenuItem key={0} value={0}>
                      <em>{t("none")}</em>
                    </MenuItem>
                    {buildCatTree(catsByType).map((i) => (
                      <MenuItem key={i.id} value={i.id}>
                        {i.name}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
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
                    {enums.status.map((i) => (
                      <MenuItem key={i.value} value={i.key}>
                        {t(i.key)}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              </Grid>
              <Grid item md={6}>
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.isontop}
                      onChange={handleChange}
                      name="isontop"
                      color="primary"
                    />
                  }
                  label={t("top")}
                />
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.isonright}
                      onChange={handleChange}
                      name="isonright"
                      color="primary"
                    />
                  }
                  label={t("right")}
                />
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.isonbottom}
                      onChange={handleChange}
                      name="isonbottom"
                      color="primary"
                    />
                  }
                  label={t("bottom")}
                />
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.isonleft}
                      onChange={handleChange}
                      name="isonleft"
                      color="primary"
                    />
                  }
                  label={t("left")}
                />
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.isoncenter}
                      onChange={handleChange}
                      name="isoncenter"
                      color="primary"
                    />
                  }
                  label={t("center")}
                />
              </Grid>
              <Grid item md={6}>
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.ispopular}
                      onChange={handleChange}
                      name="ispopular"
                      color="primary"
                    />
                  }
                  label={t("popular")}
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="shortdescription"
                  label={t("short-description")}
                  value={item.shortdescription}
                  onChange={handleChange}
                  variant="outlined"
                  multiline
                  rows={2}
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="metakey"
                  label={t("meta-keyword")}
                  value={item.metakey}
                  onChange={handleChange}
                  variant="outlined"
                  multiline
                  rows={2}
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="metatitle"
                  label={t("meta-title")}
                  value={item.metatitle}
                  onChange={handleChange}
                  variant="outlined"
                  multiline
                  rows={2}
                />
              </Grid>
              <Grid item md={6}>
                <TextField
                  name="metadescription"
                  label={t("meta-description")}
                  value={item.metadescription}
                  onChange={handleChange}
                  variant="outlined"
                  multiline
                  rows={2}
                />
              </Grid>
              <Grid item md={12}>
                {
                  <Editor
                    name="description"
                    label={t("description")}
                    data={item.description}
                    onChange={handleChange}
                  />
                }
              </Grid>
            </Grid>
          </form>
        )}
      </EditFormContainer>
    </>
  );
}
