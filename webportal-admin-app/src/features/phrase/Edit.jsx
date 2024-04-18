import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import { TextField, Grid, FormControlLabel, Checkbox } from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initPhrase,
  unselect,
  phraseSelector,
} from "../../redux/phrase/phraseSlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
} from "../../redux/phrase/phraseAsyncThunk";
import { useSelector, useDispatch } from "react-redux";
import Editor from "../shared/Editor";
import { applicationSelector } from "../../redux/application/applicationSlice";
import ValidatorSummary from "../shared/ValidatorSummary";
import { useTranslation } from "react-i18next";
import EditFormContainer from "../shared/EditFormContainer";
import { equals } from "../shared/utils";

export default function Edit() {
  const history = useHistory();
  const classes = useStyles();
  const { id } = useParams();
  const [item, setItem] = useState(initPhrase);
  const phrase = useSelector(phraseSelector);
  const application = useSelector(applicationSelector);
  const dispatch = useDispatch();
  const [removeHtml, setRemoveHtml] = useState(false);
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (id) {
      if (!equals(phrase.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setItem(phrase.item);
  }, [phrase.item]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/phrase");
  };

  const RemoveAllHtml = (str) => {
    let result = str.replace(/(<([^>]+)>)/gi, "");
    return result.trim();
  };

  const handleSave = () => {
    if (!isValid()) return;

    let objItem = {
      ...item,
      websiteid: application.website.id,
      languageid: application.languageid,
    };
    if (removeHtml) {
      objItem = { ...item, value: RemoveAllHtml(item.value) };
    }

    if (item.id) {
      dispatch(updateAsync(objItem));
    } else {
      dispatch(createAsync(objItem));
    }
    dispatch(unselect());
    history.push("/phrase");
  };

  const isValid = () => {
    let arr = [];
    if (item.key === "") {
      arr = arr.concat(t("field-cannot-empty", { fieldName: t("key") }));
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
        loading={phrase.loading}
      >
        {(equals(item.id, id) || id === undefined) && !phrase.loading && (
          <form autoComplete="off" className={classes.form}>
            <Grid container spacing={3}>
              <ValidatorSummary errors={errors} />
              <Grid item md={12}>
                <TextField
                  required
                  name="key"
                  label={t("key")}
                  value={item.key}
                  onChange={handleChange}
                  variant="outlined"
                />
              </Grid>

              <Grid item md={12}>
                {
                  <Editor
                    data={item.value}
                    onChange={handleChange}
                    name="value"
                    label={t("value")}
                    require
                  />
                }
              </Grid>
              <Grid item md={12}>
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={item.ispin}
                      onChange={handleChange}
                      name="ispin"
                      color="primary"
                    />
                  }
                  label={t("pin-to-top")}
                />
              </Grid>
              <Grid item md={12}>
                <FormControlLabel
                  control={
                    <Checkbox
                      checked={removeHtml}
                      onChange={() => setRemoveHtml(!removeHtml)}
                      name="removehtml"
                      color="primary"
                    />
                  }
                  label={t("remove-html")}
                />
              </Grid>
            </Grid>
          </form>
        )}
      </EditFormContainer>
    </>
  );
}
