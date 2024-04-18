import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import {
  TextField,
  Button,
  Grid,
  Divider,
  MenuItem,
  FormControl,
  InputLabel,
  Select,
  FormControlLabel,
  Checkbox,
} from "@material-ui/core";
import { AddCircle } from "@material-ui/icons";
import useStyles from "../shared/styles";
import {
  initProductType,
  unselect,
  productTypeSelector,
} from "../../redux/productType/productTypeSlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
} from "../../redux/productType/productTypeAsyncThunk";

import { enumSelector } from "../../redux/enum/enumSlice";
import { useSelector, useDispatch } from "react-redux";
import { applicationSelector } from "../../redux/application/applicationSlice";
import ValidatorSummary from "../shared/ValidatorSummary";
import { useTranslation } from "react-i18next";
import EditFormContainer from "../shared/EditFormContainer";
import { equals } from "../shared/utils";

export default function Edit() {
  const history = useHistory();
  const classes = useStyles();
  const { id } = useParams();
  const [item, setItem] = useState(initProductType);
  const enums = useSelector(enumSelector);
  const productType = useSelector(productTypeSelector);
  const application = useSelector(applicationSelector);
  const dispatch = useDispatch();
  const [totalText, setTotalText] = useState([1]);
  const [totalDesc, setTotalDesc] = useState([1]);
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (id) {
      if (!equals(productType.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    let currItem = productType.item;
    setItem(currItem);

    let i = 1;
    let countText = [];
    for (i = 1; i <= 20; i++) {
      if (currItem[`text${i}`] !== "") {
        countText = countText.concat(i);
      }
    }
    if (countText.length > 0) {
      setTotalText(countText);
    }

    let countDesc = [];
    for (i = 1; i <= 10; i++) {
      if (currItem[`desc${i}`] !== "") {
        countDesc = countDesc.concat(i);
      }
    }
    if (countDesc.length > 0) {
      setTotalDesc(countDesc);
    }
  }, [productType.item]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/product-type");
  };

  const handleSave = () => {
    if (!isValid()) return;

    if (item.id) {
      dispatch(updateAsync(item));
    } else {
      dispatch(
        createAsync({
          ...item,
          websiteid: application.website.id,
          languageid: application.languageid,
        })
      );
    }
    dispatch(unselect());
    history.push("/product-type");
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

  const increaseText = () => {
    setTotalText(totalText.concat(totalText.length + 1));
  };
  const increaseDesc = () => {
    setTotalDesc(totalDesc.concat(totalDesc.length + 1));
  };
  return (
    <>
      <EditFormContainer
        handleCancel={handleCancel}
        handleSave={handleSave}
        loading={productType.loading}
      >
        <form autoComplete="off" className={classes.form}>
          <Grid container spacing={3}>
            <ValidatorSummary errors={errors} />
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
                required
                name="code"
                label={t("code")}
                value={item.code}
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
              <FormControlLabel
                control={
                  <Checkbox
                    checked={item.ispublic}
                    onChange={handleChange}
                    name="ispublic"
                    color="primary"
                  />
                }
                label={t("public")}
              />
            </Grid>
            <Grid item md={12}>
              <Divider />
            </Grid>
          </Grid>

          <Grid container spacing={3}>
            <Grid item md={6} className={classes.columnSpacing}>
              {totalText.map((i) => (
                <Grid item md={12}>
                  <TextField
                    key={i}
                    name={`text${i}`}
                    label={`Text${i}`}
                    value={item[`text${i}`]}
                    onChange={handleChange}
                    variant="outlined"
                  />
                </Grid>
              ))}
              <Button
                variant="contained"
                color="primary"
                size="small"
                className={classes.button}
                startIcon={<AddCircle />}
                onClick={increaseText}
              >
                {t("add-text")}
              </Button>
            </Grid>
            <Grid item md={6} className={classes.columnSpacing}>
              {totalDesc.map((i) => (
                <Grid item md={12}>
                  <TextField
                    name={`desc${i}`}
                    label={`Desc${i}`}
                    value={item[`desc${i}`]}
                    onChange={handleChange}
                    variant="outlined"
                  />
                </Grid>
              ))}
              <Button
                variant="contained"
                color="primary"
                size="small"
                className={classes.button}
                startIcon={<AddCircle />}
                onClick={increaseDesc}
              >
                {t("add-desc")}
              </Button>
            </Grid>
          </Grid>
        </form>
      </EditFormContainer>
    </>
  );
}
