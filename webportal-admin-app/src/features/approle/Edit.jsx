import React, { useEffect, useState } from "react";

import { useParams, useHistory } from "react-router-dom";
import { TextField, Grid } from "@material-ui/core";
import useStyles from "../shared/styles";
import {
  initAppRole,
  unselect,
  approleSelector,
} from "../../redux/approle/approleSlice";
import {
  createAsync,
  getByIdAsync,
  updateAsync,
} from "../../redux/approle/approleAsyncThunk";
import { useSelector, useDispatch } from "react-redux";
import ValidatorSummary from "../shared/ValidatorSummary";
import { useTranslation } from "react-i18next";
import EditFormContainer from "../shared/EditFormContainer";
import { equals } from "../shared/utils";

export default function Edit() {
  const history = useHistory();
  const classes = useStyles();
  const { id } = useParams();
  const [item, setItem] = useState(initAppRole);
  const approle = useSelector(approleSelector);
  const dispatch = useDispatch();
  const [errors, setErrors] = useState([]);
  const { t } = useTranslation();

  useEffect(() => {
    if (id) {
      if (!equals(approle.item.id, id)) {
        dispatch(getByIdAsync(id));
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    setItem(approle.item);
  }, [approle.item]);

  const handleChange = (e) => {
    const { name, value, checked, type } = e.target;
    setItem({ ...item, [name]: ["checkbox"].includes(type) ? checked : value });
  };

  const handleCancel = () => {
    dispatch(unselect());
    history.push("/approle");
  };

  const handleSave = () => {
    if (!isValid()) return;

    if (item.id) {
      dispatch(
        updateAsync({ ...item, normalizedname: item.name.toLowerCase() })
      );
    } else {
      dispatch(
        createAsync({ ...item, normalizedname: item.name.toLowerCase() })
      );
    }
    dispatch(unselect());
    history.push("/approle");
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
        loading={approle.loading}
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
                multiline
                rows={2}
                name="description"
                label={t("description")}
                value={item.description}
                onChange={handleChange}
                variant="outlined"
              />
            </Grid>
          </Grid>
        </form>
      </EditFormContainer>
    </>
  );
}
