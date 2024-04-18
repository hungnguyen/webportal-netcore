import {
  Checkbox,
  Collapse,
  List,
  ListItem,
  ListItemIcon,
  ListItemSecondaryAction,
  ListItemText,
} from "@material-ui/core";
import { ExpandLess, ExpandMore } from "@material-ui/icons";
import React, { useEffect, useState } from "react";
import useStyles from "./styles";
import CatListView from "./CatListViewCheckbox";
import { equals } from "./utils";

const CatListItemCheckbox = ({
  item,
  all,
  className,
  selectedCats,
  onChange,
}) => {
  const [open, setOpen] = useState(true);
  const classes = useStyles();
  const [list, setList] = useState([]);

  const handleClick = () => {
    setOpen(!open);
  };

  useEffect(() => {
    setList(all.filter((i) => equals(i.parentid, item.id)));
  }, [all, item.id]);

  return (
    <>
      <ListItem button onClick={handleClick} className={className}>
        <ListItemIcon>
          {list.length > 0 && (open ? <ExpandLess /> : <ExpandMore />)}
        </ListItemIcon>
        <ListItemText primary={item.name} />
        <ListItemSecondaryAction>
          <Checkbox
            edge="start"
            checked={selectedCats.includes(item.id)}
            tabIndex={-1}
            disableRipple
            onChange={() => onChange(item.id)}
          />
        </ListItemSecondaryAction>
      </ListItem>
      {list.length > 0 && (
        <Collapse in={open} timeout="auto" unmountOnExit>
          <List component="div" disablePadding className={classes.nested6}>
            <CatListView
              all={all}
              parentid={item.id}
              selectedCats={selectedCats}
              onChange={onChange}
            />
          </List>
        </Collapse>
      )}
    </>
  );
};

export default CatListItemCheckbox;
