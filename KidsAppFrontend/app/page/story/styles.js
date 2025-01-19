import color from '@/app/styles/color';
import { StyleSheet, Dimensions } from 'react-native';

const { height, width } = Dimensions.get('window');

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  storyContainer: {
    height: height / 1.7,
    width: width - 40,
    justifyContent: 'center',
    alignItems: 'center', 
    padding: 20,
    backgroundColor: 'white', 
    borderRadius: 10, 
    marginLeft:20,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 10,
    padding:10
  },
  story: {
    fontSize: 16,
    color: color.GirlColors.Secondary,
    marginBottom: 30,
    lineHeight: 24,
  },
  errorText: {
    fontSize: 18,
    color: 'red',
    textAlign: 'center',
    marginTop: 20,
  },
});

export default styles;
