from biomedlexicon import search_biomedlexicon
import symptom_api
from farmakeia import get_pharmacies, get_url
from flask import Flask
from flask import jsonify

app = Flask(__name__)
api_version = '/ath/api/v0.1/'


@app.route(api_version+'biomedlexicon/<string:search_term>', methods=['GET'])
def get_biomedlexicon_translation(search_term):
    return jsonify(search_biomedlexicon(search_term))


@app.route(api_version+'farmakeia.gr/<string:pharmacy_type>/<string:district>/<string:sub_district>', methods=['GET'])
def get_farmakeia_gr_pharmacies(pharmacy_type, district, sub_district):
    return jsonify(get_pharmacies(get_url(pharmacy_type, district, sub_district)))


@app.route(api_version+'symptoms/', methods=['GET'])
def _():
    return jsonify(symptom_api.get_body_part_list())


@app.route(api_version+'symptoms/get', methods=['GET'])
def __():
    return jsonify(symptom_api.get_body_url())


@app.route('/', methods=['GET'])
def default_route():
    return jsonify({'api_version': '0.1'})


if __name__ == "__main__":
    app.run(debug=True)
