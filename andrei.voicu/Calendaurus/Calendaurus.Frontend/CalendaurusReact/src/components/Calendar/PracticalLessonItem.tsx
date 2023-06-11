import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from "@mui/material";
import { useState } from "react";
import { PracticalLessonEvent, PracticalLessonEventEnrollment } from "../../actions/models";
import { enrollStudent, unenrollStudent } from "../../actions/api";
import { useMsal } from "@azure/msal-react";
import styled from "styled-components";
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import RemoveCircleOutlineIcon from '@mui/icons-material/RemoveCircleOutline';

type PracticalLessonItemProps = {
  disciplineName?: string;
  practicalLessonDescription?: string;
  practicalLessonEvent?: PracticalLessonEvent;
  isEnrolled?: boolean;
  updateIsEnrolled?: (enrolled?: boolean) => void;
};

const Item = styled.div<{ isEnrolled?: boolean }>`
  background-color: ${props => props.isEnrolled ? "#83de92" : "white"};
  padding: 0rem 0.25rem;
  cursor: pointer;
  &:hover {
    background-color: ${props => props.isEnrolled ? "#74da74" : "#dfdfdf"};
  }
  display: flex;
  align-items: center;
  box-shadow: #9e9e9e 0.1rem 0.1rem 5px;
`;

const TextItem = styled.span`
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;
    padding-left: 0.25rem;
`;

export const PracticalLessonItem = ({
  disciplineName,
  practicalLessonDescription,
  practicalLessonEvent,
  isEnrolled,
  updateIsEnrolled,
}: PracticalLessonItemProps) => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { instance } = useMsal();

  const handleClose = (enrolled?: boolean) => {
    setIsModalOpen(false);
    enrolled !== undefined && updateIsEnrolled?.();
  };

  const handleSave = () => {
    const practicalLessonEventId = practicalLessonEvent?.id;
    if (practicalLessonEventId) {
      const practicalLessonEnrollment: PracticalLessonEventEnrollment = {
        practicalLessonEventId: practicalLessonEventId,
      };
      if (isEnrolled) {
        unenrollStudent(
          instance,
          () => handleClose(false),
          practicalLessonEventId
        );
      } else {
        enrollStudent(
          instance,
          () => handleClose(true),
          practicalLessonEnrollment
        );
      }
    }
  };

  return (
    <>
      <Box onClick={() => setIsModalOpen(true)}>
        <Item isEnrolled={isEnrolled} title={
          isEnrolled
            ? ("Unenroll from " + disciplineName + ": " + practicalLessonDescription)
            : ("Enroll to " + disciplineName + ": " + practicalLessonDescription)
        }>
          {isEnrolled
            ? <RemoveCircleOutlineIcon fontSize="small" color="error" />
            : <AddCircleOutlineIcon fontSize="small" color="primary" />
          }
          <TextItem>{disciplineName}: {practicalLessonDescription}</TextItem>
        </Item>
      </Box>
      <Dialog
        open={isModalOpen}
        onClose={() => handleClose()}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">
          {disciplineName}: {practicalLessonDescription}
        </DialogTitle>
        <DialogContent>
          <DialogContentText id="alert-dialog-description">
            Are you sure you want to {isEnrolled ? "unenroll" : "enroll"} to
            this class?
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleSave}>
            {isEnrolled ? "Unenroll" : "Enroll"}
          </Button>
          <Button onClick={() => handleClose()}>Cancel</Button>
        </DialogActions>
      </Dialog>
    </>
  );
};
