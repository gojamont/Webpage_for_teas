from flask import Flask, render_template, url_for, send_from_directory
import os

app = Flask(__name__)

@app.route('/favicon.ico')
def favicon():
    return send_from_directory(os.path.join(app.root_path, 'static'),
                          'favicon.ico',mimetype='image/vnd.microsoft.icon')
@app.route('/')
def index():
    return render_template("index.html")

@app.route('/meniu')
def meniu():
    return render_template("meniu.html")

@app.route('/vaistas_ir_maistas_visada_salia')
def edu_1():
    return render_template("vaistas_maistas_salia.html")

@app.route('/vaistazoles_prie_namu')
def edu_2():
    return render_template("vaistazoles_prie_namu.html")

@app.route('/bobvakariai')
def edu_3():
    return render_template("bobvakariai.html")

@app.route('/nuoma')
def nuoma():
    return render_template("nuoma.html")

@app.route('/kontaktai')
def kontaktai():
    return render_template("kontaktai.html")

@app.route('/galerija')
def galerija():
    return render_template("galerija.html")

if __name__ == "__main__":
    app.run(host='0.0.0.0', port=5000, debug=True)
