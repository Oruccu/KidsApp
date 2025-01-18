
import { StyleSheet, Dimensions } from 'react-native';
import color from '@/app/styles/color.js';

const windowWidth = Dimensions.get('window').width;

const styles = StyleSheet.create({
  container: {
    borderRadius: 30,
    marginBottom:30,
    justifyContent: 'center',
    alignItems: 'center',
    margin:5
  },
  btnName: {
    padding: 10,
    color: 'white',
    width: windowWidth / 3,
    textAlign: 'center',
    textAlignVertical: 'center',
    fontSize: 20,
    fontWeight: 'bold'
  }
});

export default styles;
