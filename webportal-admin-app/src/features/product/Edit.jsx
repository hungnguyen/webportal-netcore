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
  Tabs,
  Tab,
  List
} from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initProduct,
  unselect,
  productSelector,
} from "../../redux/product/productSlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
} from "../../redux/product/productAsyncThunk";

import { enumSelector } from "../../redux/enum/enumSlice";
import { useSelector, useDispatch } from "react-redux";
import Editor from "../shared/Editor";
import { accountSelector } from "../../redux/account/accountSlice";
import { productTypeSelector } from "../../redux/productType/productTypeSlice";
import { categorySelector } from "../../redux/category/categorySlice";
import { getAllAsync } from "../../redux/category/categoryAsyncThunk";
import {
  productInCategorySelector,
  clearAll,
} from "../../redux/productInCategory/productInCategorySlice";
import { getByProductIdAsync as getProductInCat } from "../../redux/productInCategory/productInCategoryAsyncThunk";
import { initProductType } from "../../redux/productType/productTypeSlice";
import TabPanel from "../shared/TabPanel";
import CatListViewCheckbox from "../shared/CatListViewCheckbox";
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
  const { id, type } = useParams();
  const [item, setItem] = useState(initProduct);
  const product = useSelector(productSelector);
  const enums = useSelector(enumSelector);
  const account = useSelector(accountSelector);
  const category = useSelector(categorySelector);
  const productInCategory = useSelector(productInCategorySelector);
  const application = useSelector(applicationSelector);

  const dispatch = useDispatch();
  const [imageSrc, setImageSrc] = useState("");
  const [imageFile, setImageFile] = useState(null);
  const productTypeState = useSelector(productTypeSelector);
  const [productType, setProductType] = useState(initProductType);
  const [tabIndex, setTabIndex] = useState(0);
  const [catsByType, setCatsByType] = useState([]);
  const [catIds, setCatIds] = useState([]);
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  const handleTabChange = (event, newValue) => {
    setTabIndex(newValue);
  };

  useEffect(() => {
    if (id) {
      if (!equals(product.item.id, id)) {
        dispatch(getByIdAsync(id));
        dispatch(getProductInCat(id));
      } else {
        setItem(product.item);
      }
    } else {
      dispatch(clearAll());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (productTypeState.list.length > 0) {
      setProductType(productTypeState.list.find((i) => i.code === type));
    }
  }, [type, productTypeState]);

  useEffect(() => {
    if (category.list.length === 0) {
      dispatch(getAllAsync());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    let types = [type];
    if (type === "PRD") types = types.concat("FIL");
    setCatsByType(category.list.filter((i) => types.includes(i.typecode)));
  }, [type, category.list]);

  useEffect(() => {
    setCatIds(productInCategory.list.map((i) => i.categoryid));
  }, [productInCategory.list]);

  useEffect(() => {
    setItem(product.item);
    if (product.item.image !== "") {
      setImageSrc(`${application.imageBaseAddress}/${product.item.image}`);
    }
  }, [product.item, application.imageBaseAddress]);

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
    history.push(`/product/${type}`);
  };

  const handleSave = () => {
    if (!isValid()) return;

    if (item.id) {
      dispatch(
        updateAsync({
          item: {
            ...item,
            urlname: item.urlname === "" ? getUrlName(item.name) : item.urlname,
            updatedby: account.profile.username,
            dateupdated: new Date(),
          },
          imageData: getFormData(),
          inCategories: catIds,
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
            typecode: type,
            websiteid: application.website.id,
            languageid: application.languageid,
          },
          imageData: getFormData(),
          inCategories: catIds,
        })
      );
    }
    dispatch(unselect());
    history.push(`/product/${type}`);
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

  const handleSelectCat = (value) => {
    const currentIndex = catIds.indexOf(value);
    const newChecked = [...catIds];

    if (currentIndex === -1) {
      newChecked.push(value);
    } else {
      newChecked.splice(currentIndex, 1);
    }

    setCatIds(newChecked);
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
  return (
    <>
      <EditFormContainer
        handleCancel={handleCancel}
        handleSave={handleSave}
        loading={product.loading}
      >
        {product.loading && (<Loading />)}
        {(equals(item.id, id) || id === undefined) && !product.loading && (
          <form autoComplete="off" className={classes.form}>
            <Grid container spacing={3}>
              <ValidatorSummary errors={errors} />
              <Grid item md={6}>
                <Typography>{t("image")}:</Typography>
              </Grid>
              <Grid item md={6}>
                <Typography>{t("in-categories")}:</Typography>
              </Grid>
              <Grid item md={6}>
                <Grid item md={6}>
                  {imageSrc !== "" && (
                    <img src={imageSrc} alt="" className={classes.image} />
                  )}
                </Grid>
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
              <Grid item md={6} className={classes.height400}>
                <List component="nav">
                  <CatListViewCheckbox
                    all={catsByType}
                    parentid={0}
                    className=""
                    selectedCats={catIds}
                    onChange={handleSelectCat}
                  />
                </List>
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
                <TextField
                  name="replateproduct"
                  label={t("related-product")}
                  value={item.replateproduct}
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
                      checked={item.ishot}
                      onChange={handleChange}
                      name="ishot"
                      color="primary"
                    />
                  }
                  label={t("hot")}
                />
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.isfeature}
                      onChange={handleChange}
                      name="isfeature"
                      color="primary"
                    />
                  }
                  label={t("feature")}
                />
              </Grid>
              {[
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18,
                19, 20,
              ].map(
                (i) =>
                  productType[`text${i}`] !== "" && (
                    <Grid item md={6} key={i}>
                      <TextField
                        name={`text${i}`}
                        label={productType[`text${i}`]}
                        value={item[`text${i}`]}
                        onChange={handleChange}
                        variant="outlined"
                      />
                    </Grid>
                  )
              )}
              <Grid item md={12}>
                <Tabs value={tabIndex} onChange={handleTabChange}>
                  {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(
                    (i) =>
                      productType[`desc${i}`] !== "" && (
                        <Tab
                          label={productType[`desc${i}`]}
                          id={`tab-${i}`}
                          key={i}
                        />
                      )
                  )}
                </Tabs>

                {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(
                  (i) =>
                    productType[`desc${i}`] !== "" && (
                      <TabPanel value={tabIndex} index={i - 1} key={i}>
                        <Editor
                          name={`desc${i}`}
                          label=""
                          data={item[`desc${i}`]}
                          onChange={handleChange}
                        />
                      </TabPanel>
                    )
                )}
              </Grid>
            </Grid>
          </form>
        )}
      </EditFormContainer>
    </>
  );
}
