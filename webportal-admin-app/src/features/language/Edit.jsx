import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import { TextField, Grid, FormControlLabel, Checkbox } from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initLanguage,
  unselect,
  languageSelector,
} from "../../redux/language/languageSlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
} from "../../redux/language/languageAsyncThunk";
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
  const [item, setItem] = useState(initLanguage);
  const language = useSelector(languageSelector);
  const application = useSelector(applicationSelector);
  const dispatch = useDispatch();
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (id) {
      if (!equals(language.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setItem(language.item);
  }, [language.item]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/language");
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
        })
      );
    }
    dispatch(unselect());
    history.push("/language");
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
        loading={language.loading}
      >
        <form autoComplete="off" className={classes.form}>
          <Grid container spacing={3}>
            <ValidatorSummary errors={errors} />
            <Grid item md={12}>
              <TextField
                required
                name="name"
                label={t("name")}
                value={item.name}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>
            <Grid item md={12}>
              <TextField
                required
                name="code"
                label={t("code")}
                value={item.code}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>

            <Grid item md={12}>
              <FormControlLabel
                control={
                  <Checkbox
                    checked={item.isdefault}
                    onChange={handleChange}
                    name="isdefault"
                    color="primary"
                  />
                }
                label={t("default-language")}
              />
            </Grid>
          </Grid>
        </form>
      </EditFormContainer>
    </>
  );
}
