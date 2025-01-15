import { useSelector } from 'react-redux';
import color from '@/app/styles/color.js';

const useTheme = () => {
  const currentMode = useSelector((state) => state.mode.currentMode);
  return currentMode === 'Boy' ? color.BoyColors : color.GirlColors;
};

export default useTheme;
