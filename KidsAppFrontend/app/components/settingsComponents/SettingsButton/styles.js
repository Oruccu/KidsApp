// src/app/pages/styles.js
import { StyleSheet } from 'react-native';
import color from '@/app/styles/color.js';

const BaseStyles = StyleSheet.create({
  container: { 
    paddingVertical: 12,                   
    borderRadius: 20,              
    alignItems: 'center',         
    justifyContent: 'center',    
    padding:10,
    width:100,
    marginRight:20,
  },
  title: {
    fontSize:20,
    fontWeight:'bold',
    color: '#fff', 
  },
  modeContainer: {
    marginVertical: 20,
  },
  modeText: {
    fontSize: 18,
    fontWeight: '600',
  },
  buttonContainer: {
    flexDirection: 'row',
  },
});

export default {
  ...BaseStyles,
  BoyButton: StyleSheet.create({
    container: {
      ...BaseStyles.container,
      backgroundColor: color.BoyColors.Primary,
    },
    title: {
      ...BaseStyles.title,
      color: color.BoyColors.Secondary,
    },
  }),
  GirlButton: StyleSheet.create({
    container: {
      ...BaseStyles.container,
      backgroundColor: color.GirlColors.Primary,
    },
    title: {
      ...BaseStyles.title,
      color: color.GirlColors.Secondary,
    },
  }),
};
